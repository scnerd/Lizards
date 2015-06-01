using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Collections.Concurrent;

namespace Lizards
{
    // TODO: Comment out this line to return to using the actual serial port library
    using SerialPort = FakeSerialPort;
    using System.Timers;
    public delegate void NewAmbientTempHandler(double newTemp);

    /// <summary>
    /// FakeSerialPort takes the place of the actual SerialPort library, supporting all needed methods and reporting fake temperatures using the protocol defined for this experiment
    /// </summary>
    public class FakeSerialPort
    {
        BlockingCollection<byte> ReadyToRead = new BlockingCollection<byte>();
        Queue<byte> Written = new Queue<byte>();

        static FakeSerialPort ()
        {
            LizardData.CurrentAmbientTemp = 35;
        }

        public FakeSerialPort(string a, int b, Parity c)
        {
            IsOpen = false;
        }

        internal static string[] GetPortNames()
        {
            return new[] {"COM?"};
        }

        public StopBits StopBits { get; set; }

        public int DataBits { get; set; }

        public bool DtrEnable { get; set; }

        public bool IsOpen { get; private set; }

        internal void Open()
        {
            IsOpen = true;
            demo_junk();
        }

        internal int ReadByte()
        {
            return IsOpen ? ReadyToRead.Take() : -1;
        }

        internal void Close()
        {
            IsOpen = false;
        }

        internal void Write(byte[] data, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
                Written.Enqueue(data[i]);
        }


        public void demo_junk()
        {
            var timer1 = new Timer();
            ds = new double[ArduinoCommunicator.Lizards.Length + 1];
            rnd = new Random();
            timer1.Interval = 100;
            timer1.Elapsed += timer1_Tick;
            timer1.Start();
        }
        private Random rnd;
        private double[] ds;
        private double MAX = 50, MIN = 27;
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*
             * In short, for each temperature (a value between 0 and 1), randomly move it around and
             * report that temperature back through the serial port buffer (as a ushort scaled correctly
             * as defined by the current thermometer settings)
             */

            for (int i = 0; i < ds.Length; i++)
            {
                bool Up = rnd.NextDouble() > ds[i];
                ds[i] += rnd.NextDouble() * 0.1d * (Up ? 1 : -1);
            }
            Push(0xDEAD);
            Push(0xBEEF);
            foreach(double d in ds)
            {
                if (d == ds[0])
                    Push(ArduinoCommunicator.ConvertFromAmbientTemp(d * (MAX - MIN) + MIN));
                else
                    Push(ArduinoCommunicator.ConvertFromLizardTemp(d * (MAX - MIN) + MIN));
            }
            Push(0xDEAD);
            Push(0xBEEF);
        }

        private void Push(ushort data)
        {
            byte[] bytes = BitConverter.GetBytes(data);
            foreach (byte b in bytes)
                ReadyToRead.Add(b);
        }
    }

    /// <summary>
    /// Supports all communication and protocols needed to talk to an Arduino programmed with the Lizard Experiment code.
    /// This reads temperatures from the Arduino and writes signals to it, updating Lizard data objects and the ambient
    /// temperature when new data is received. Note that this class is also responsible for creating and maintaining the
    /// Lizard data objects. This class is a static singleton, that is, only the static version of this class exists, and
    /// all other code refers to this single static instance.
    /// </summary>
    public static class ArduinoCommunicator
    {
        // Defines an event that gets triggered when a new ambient temperature reading is received
        public static event NewAmbientTempHandler OnNewAmbientTemp;

        // Defines the binary values for each signal that can be sent to the Arduino
        private enum ArduinoSignal
        {
            Hold = (ushort)0x0001,
            Start = (ushort)0x0002,
            Stop = (ushort)0x0003
        }

        // The SerialPort connection settings, must be synchronized with how the Arduino sets up its serial communication
        public const int BAUD_RATE = 9600;
        public const Parity PARITY = Parity.Even;
        public static readonly StopBits STOP_BITS = StopBits.One;
        public const int BITS_PER_DATA = 8;

        // Default value for how many seconds should elapse before saving another temperature reading for each lizard
        public const int DEFAULT_SAVE_INTERVAL = 15;

        // Defines the thermometer settings for the manifold (ambient) and lizard thermometers
        //      Scale: 1 / How many integer values elapse between a single degree celsius (e.g., a 1/16 deg C resolution thermometer would use 1/16 as the scale)
        //      Base: The temperature in deg C when the thermometer reports the 0 value
        public const double AMBIENT_SCALE = 1/16d, AMBIENT_BASE = 0d;
        public const double LIZARD_SCALE = 1/16d, LIZARD_BASE = 0d;

        // The serial port that the Arduino is plugged into
        private static SerialPort Port;

        // Some experiment state variables
        private static bool KeepRunning;
        private static bool ExperimentRunning = false;
        private static bool ExperimentStarted = false;
        private static bool AlreadyWrote = false;

        // The master list of lizards
        public static LizardData[] Lizards { get; private set; }

        // The timestamp for when the experiment is started (when the temperature ramp begins)
        public static DateTime StartTime { get; private set; }

        // A string that records data that's sent from the Arduino in hex format, for debugging
        private static StringBuilder DataReadLog = new StringBuilder();

        /// <summary>
        /// Sets up the event handler for when the program is shut down, forcefully or gently
        /// </summary>
        static ArduinoCommunicator()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, args) =>
            {
                try
                {
                    SendStopSignal();
                }
                catch (Exception)
                {
                }
                if (Port != null && Port.IsOpen)
                    Port.Close();

                try
                {
                    SaveResults(DEFAULT_SAVE_INTERVAL, false);
                }
                catch (Exception)
                {
                }
            });
        }

        /// <summary>
        /// Retrieves all possible serial ports that the Arduino could be plugged into
        /// </summary>
        /// <returns>A list of serial port names</returns>
        public static string[] GetPossiblePorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// Connects to the Arduino, resetting it and spinning off a listener thread to retrieve data from it
        /// </summary>
        /// <param name="ToConnect">The name of the serial port to connect to</param>
        public static void Connect(string ToConnect)
        {
            ReportDebug("Connecting to Arduino");
            // Prepare settings to connect to Arduino
            Port = new SerialPort(ToConnect, BAUD_RATE, PARITY);
            Port.StopBits = STOP_BITS;
            Port.DataBits = BITS_PER_DATA;
            // Resets the Arduino upon connecting
            Port.DtrEnable = true;
            // Open the port (throws System.UnauthorizedAccessException if the port is already open elsewhere)
            Port.Open();

            // Spin up listener thread
            KeepRunning = true;
            Task ListenToArduino = new Task(() =>
            {
                ReportDebug("Beginning to listen for data from Arduino");
                while (KeepRunning)
                {
                    // Get the temperature readings from the serial signal
                    double[] temps = ReadTemps();
                    // Deal with the ambient temperature first (always the first value)
                    if(OnNewAmbientTemp != null)
                        OnNewAmbientTemp(temps[0]);
                    LizardData.CurrentAmbientTemp = temps[0];
                    temps = temps.Skip(1).ToArray(); // Drop the first value, since we've used it already
                    lock (Lizards)
                    {
                        // Handle all the lizard temps
                        foreach (
                            var liz_temp in Lizards.Zip(temps, (liz, temp) => new Tuple<LizardData, double>(liz, temp)))
                            liz_temp.Item1.Update(liz_temp.Item2);
                    }
                }
            });
            ListenToArduino.Start();
        }

        /// <summary>
        /// Creates the lizard objects, initializing the Lizards array
        /// </summary>
        /// <param name="NumLizards">The number of lizards to create</param>
        /// <returns>The Lizards array, if desired</returns>
        public static LizardData[] InitializeLizards(int NumLizards)
        {
            Lizards = new LizardData[NumLizards];
            for(int i = 0; i < NumLizards; i++)
                Lizards[i] = new LizardData(i);

            return Lizards;
        }

        /// <summary>
        /// Adds a text writer to receive debug output
        /// </summary>
        /// <param name="Writer">The writer to be called when debug output is available</param>
        public static void AddDebugOutput(TextWriter Writer)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(Writer));
        }

        /// <summary>
        /// Makes a report through the debug writers
        /// </summary>
        /// <param name="format">A string that optionally contains format parameters</param>
        /// <param name="args">Formatting parameters</param>
        internal static void ReportDebug(string format, params object[] args)
        {
            Debug.WriteLine("{0} - {1}", StartTime != default(DateTime) ? (DateTime.Now - StartTime).ToString(@"%h\:mm\:ss") : DateTime.Now.ToString("H:mm:ss"), string.Format(format, args));
        }

        /// <summary>
        /// Tells the Arduino to try to hold steady at the start temperature
        /// </summary>
        /// <param name="Temp">The temperature to hold (in degrees C)</param>
        public static void StartHoldingTemp(double Temp)
        {
            ReportDebug("Beginning to hold {0:F2}°C", Temp);
            SendHoldSignal(ConvertFromAmbientTemp(Temp));
        }

        /// <summary>
        /// Tells the Arduino to start ramping up its temperature at a steady ramp rate
        /// </summary>
        /// <param name="Ramp">The ramp rate (in degrees C per minute)</param>
        /// <param name="Target">The max temperature to reach (in degrees C)</param>
        public static void StartRampingTemp(double Ramp, double Target)
        {
            ReportDebug("Marking start time");
            ExperimentStarted = ExperimentRunning = true;
            StartTime = DateTime.Now;
            ReportDebug("Starting ramp of {0:F2}°C to {1:F2}°C", Ramp, Target);
            SendStartSignal(ConvertFromAmbientTemp(Ramp), ConvertFromAmbientTemp(Target));
            //Port.NewLine = END_OF_DATA_BLOCK;
        }

        /// <summary>
        /// Reads the specified number of bytes from the Arduino
        /// </summary>
        /// <param name="Count">The number of bytes to read</param>
        /// <returns>An array of the bytes read, which may be shorter than requested if the port is closed</returns>
        private static byte[] ReadBytes(int Count)
        {
            return Enumerable.Range(0, Count)
                .Select(idx => Port.ReadByte())
                .Where(val => val >= 0) // Ignore values given if the port is closed
                .Select(i => (byte)i)
                .ToArray();
        }

        /// <summary>
        /// Reads an unsigned short from the Arduino, since this is the size of data that we primarily work with
        /// </summary>
        /// <returns>The ushort that was read</returns>
        private static ushort ReadChunk()
        {
            ushort val = BitConverter.ToUInt16(ReadBytes(2), 0);
            DataReadLog.Append(string.Format("0x{0:X4} ", val));
            return val;
        }

        /// <summary>
        /// Reads an entire packet from the Arduino, however long it may be
        /// </summary>
        /// <returns>The packet contents</returns>
        private static ushort[] ReadMessage()
        {
            List<ushort> Messages = new List<ushort>();
            ushort a, b, c;
            a = b = c = 0;
            while (!(a == 0xDEAD && b == 0xBEEF))
            {
                a = b;
                b = ReadChunk();
            }
            b = ReadChunk();
            c = ReadChunk();
            while (!(b == 0xDEAD && c == 0xBEEF))
            {
                a = b;
                b = c;
                c = ReadChunk();
                Messages.Add(a);
            }
            return Messages.ToArray();
        }

        /// <summary>
        /// Reads a list of temperatures from the Arduino
        /// </summary>
        /// <returns>The list of temperatures, where the first element is the ambient temp and the remaining elements belong to the lizards in order</returns>
        private static double[] ReadTemps()
        {
            ushort[] Temps = ReadMessage();
            List<double> lizard_temps = new List<double>();
            try
            {
                double ambient = ConvertAmbientTemp(Temps[0]);
                lizard_temps = Lizards.Select((junk, idx) => ConvertLizardTemp(Temps[idx + 1])).ToList();
                lizard_temps.Insert(0, ambient);
            }
            catch (IndexOutOfRangeException)
            {
                ReportDebug("Failed to read temperatures: Wrong number of temperatures read from Arduino");
                DumpReadData();
            }
            return lizard_temps.ToArray();
        }

        /// <summary>
        /// Ends the experiment and commands the Arduino to stop heating
        /// </summary>
        public static void StopExperiment()
        {
            ReportDebug("Stopping experiment");
            ExperimentRunning = false;
            KeepRunning = false;
            SendStopSignal();
        }

        /// <summary>
        /// Resets the connection to the Arduino, causing it to reset and enter its initial state again
        /// </summary>
        public static void ForceStop()
        {
            ReportDebug("Resetting Arduino to terminate heating");
            ExperimentRunning = false;
            Port.Close();
            Port.Open();
        }

        /// <summary>
        /// Sends the specified ushort to the Arduino
        /// </summary>
        /// <param name="Chunk">The value to send</param>
        private static void Send(ushort Chunk)
        {
            Port.Write(BitConverter.GetBytes(Chunk), 0, 2);
        }

        /// <summary>
        /// Sends the specified signal to the Arduino
        /// </summary>
        /// <param name="Sig">The signal to send</param>
        private static void Send(ArduinoSignal Sig)
        {
            Send((ushort) Sig);
        }

        /// <summary>
        /// Sends the "Hold" signal to the Arduino
        /// </summary>
        /// <param name="Target">The temperature to hold</param>
        private static void SendHoldSignal(ushort Target)
        {
            Send(ArduinoSignal.Hold);
            Send(Target);
        }

        /// <summary>
        /// Sends the "Ramp" signal to the Arduino
        /// </summary>
        /// <param name="Ramp">The temperature rate to ramp at</param>
        /// <param name="Target">The max temperature to reach</param>
        private static void SendStartSignal(ushort Ramp, ushort Target)
        {
            Send(ArduinoSignal.Start);
            Send(Ramp);
            Send(Target);
        }

        /// <summary>
        /// Sends the "Stop" signal to the Arduino
        /// </summary>
        private static void SendStopSignal()
        {
            Send(ArduinoSignal.Stop);
        }

        /// <summary>
        /// Converts the given ambient temperature, as reported by the manifold thermometer, into degrees C
        /// </summary>
        /// <param name="Value">The reading from the the manifold thermometer</param>
        /// <returns>The equivalent degrees C</returns>
        internal static double ConvertAmbientTemp(int Value)
        {
            return Value * AMBIENT_SCALE + AMBIENT_BASE;
        }

        /// <summary>
        /// Converts the given ambient temperature, as degrees C, into the equivalent reading from the manifold thermometer
        /// </summary>
        /// <param name="Value">The temperature in degrees C</param>
        /// <returns>The equivalent reading from the thermometer</returns>
        internal static ushort ConvertFromAmbientTemp(double Value)
        {
            return (ushort) Math.Round((Value - AMBIENT_BASE)/AMBIENT_SCALE);
        }

        internal static double ConvertLizardTemp(int Value)
        {
            return Value * LIZARD_SCALE + LIZARD_BASE;
        }

        internal static ushort ConvertFromLizardTemp(double Value)
        {
            return (ushort) Math.Round((Value - LIZARD_BASE) / LIZARD_SCALE);
        }

        public static string Combine(this string Base, params string[] Others)
        {
            return Others.Aggregate((a, b) => a + Base + b);
        }

        public static string SaveResults(int ReportInterval, bool WriteEvenIfAlreadyWrote = true)
        {
            ReportDebug("Beginning to save results");

            if (AlreadyWrote && !WriteEvenIfAlreadyWrote)
            {
                ReportDebug("Already wrote to file");
                return null;
            }
            if(!ExperimentStarted)
            {
                ReportDebug("Experiement hasn't been started yet");
                return null;
            }

            StartTime = StartTime != default(DateTime) ? StartTime : new DateTime(0);

            string Dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Lizards";
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
            string Now = DateTime.UtcNow.ToString("u").Replace(":", "_");
            string Path = string.Format(@"{1}\Lizard Results {0}.csv", Now, Dir);
            StringBuilder OutputData = new StringBuilder();

            Func<DateTime, string> Format = (dt) => dt == default(DateTime) ? "" : (dt - StartTime).ToString("g");

            lock (Lizards)
            {
                OutputData.AppendLine(string.Format("Start time,{0:G}", StartTime));
                OutputData.AppendLine();

                OutputData.AppendLine(",Ambient," +
                                      ",".Combine(
                                          Lizards.Select(liz => string.Format("Lizard {0}", liz.Number + 1)).ToArray()));
                for (int i = 0; i < LizardData.NUM_EVENTS; i++)
                    OutputData.AppendLine(string.Format("{0},,{1}", LizardData.EVENTS[i],
                        ",".Combine(
                            Lizards.Select(liz => liz.MainEvents[i] == null ? "" : Format(liz.MainEvents[i].Timestamp))
                                .ToArray())));

                int mult = 0;
                Func<DateTime> CurrentTime = () => StartTime + TimeSpan.FromSeconds(mult*ReportInterval);
                while (
                    Lizards.Any(
                        liz =>
                            liz.TempRecords.Last().Timestamp > CurrentTime()))
                {
                    var CurTimeTemp = CurrentTime();
                    var LizardRecords =
                        Lizards.Select(liz => liz.TempRecords.FirstOrDefault(rec => rec.Timestamp > CurTimeTemp))
                            .ToArray();
                    OutputData.AppendLine(string.Format("{0},{1:F2},{2}", Format(CurrentTime()),
                        LizardRecords.Where(rec => rec != null).Average(rec => rec.AmbientTemp),
                        ",".Combine(LizardRecords.Select(rec => rec == null ? "" : rec.LizardTemp.ToString("F2")).ToArray())));
                    mult++;
                }

                OutputData.AppendLine();
                OutputData.AppendLine("Lizard,Timestamp,Lizard Temp, Ambient Temp, Note");
                foreach (
                    Tuple<LizardData, LizardData.Record> rec in
                        Lizards.SelectMany(
                            liz =>
                                liz.ImportantRecords.Select(rec => new Tuple<LizardData, LizardData.Record>(liz, rec)))
                            .OrderBy(rec => rec.Item2.Timestamp))
                {
                    OutputData.AppendLine(string.Format("Lizard {0},{1},{2},{3},{4}", rec.Item1.Number + 1,
                        Format(rec.Item2.Timestamp),
                        rec.Item2.LizardTemp, rec.Item2.AmbientTemp, rec.Item2.Note));
                }
            }

            File.WriteAllText(Path, OutputData.ToString());
            ReportDebug("Results saved to: {0}", Path);

            return Path;
        }

        private static void DumpReadData()
        {
            ReportDebug("Data read log:");
            ReportDebug(DataReadLog.ToString());
            DataReadLog = new StringBuilder();
        }
    }
}
