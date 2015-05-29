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

    public static class ArduinoCommunicator
    {
        

        public static event NewAmbientTempHandler OnNewAmbientTemp;

        public enum ArduinoStatus
        {
            None = (ushort)0x0000,
            Preparing = (ushort)0x0001,
            Ramping = (ushort)0x0002
        }

        private enum ArduinoSignal
        {
            Hold = (ushort)0x0001,
            Start = (ushort)0x0002,
            Stop = (ushort)0x0003,
            Status = (ushort)0x0004
        }

        public const int BAUD_RATE = 9600;
        public const Parity PARITY = Parity.Even;
        public static readonly StopBits STOP_BITS = StopBits.One;
        public const int BITS_PER_DATA = 8;
        private const string RUN_FILE = "experiment.txt";

        public const int DEFAULT_SAVE_INTERVAL = 15;
        public const double AMBIENT_SCALE = 1/16d, AMBIENT_BASE = 0d;
        public const double LIZARD_SCALE = 1/16d, LIZARD_BASE = 0d;

        private static SerialPort Port;
        private static bool KeepRunning;
        private static bool ExperimentRunning = false;
        private static bool ExperimentStarted = false;
        private static bool AlreadyWrote = false;
        public static LizardData[] Lizards { get; private set; }

        public static DateTime StartTime { get; private set; }

        private static StringBuilder DataReadLog = new StringBuilder();

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
            //StartTime = DateTime.Now; // This should get overwritten by the start of the experiment, but just in case
        }

        public static string[] GetPossiblePorts()
        {
            return SerialPort.GetPortNames();
        }

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


            KeepRunning = true;
            Task ListenToArduino = new Task(() =>
            {
                ReportDebug("Beginning to listen for data from Arduino");
                while (KeepRunning)
                {
                    double[] temps = ReadTemps();
                    OnNewAmbientTemp(temps[0]);
                    LizardData.CurrentAmbientTemp = temps[0];
                    temps = temps.Skip(1).ToArray(); // Drop the first value, since we've used it already
                    lock (Lizards)
                    {
                        foreach (
                            var liz_temp in Lizards.Zip(temps, (liz, temp) => new Tuple<LizardData, double>(liz, temp)))
                            liz_temp.Item1.Update(liz_temp.Item2);
                    }
                }
            });
            ListenToArduino.Start();
        }

        public static LizardData[] InitializeLizards(int NumLizards)
        {
            Lizards = new LizardData[NumLizards];
            for(int i = 0; i < NumLizards; i++)
                Lizards[i] = new LizardData(i);

            return Lizards;
        }

        public static void AddDebugOutput(TextWriter Writer)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(Writer));
        }

        internal static void ReportDebug(string format, params object[] args)
        {
            Debug.WriteLine("{0} - {1}", StartTime != default(DateTime) ? (DateTime.Now - StartTime).ToString(@"%h\:mm\:ss") : DateTime.Now.ToString("H:mm:ss"), string.Format(format, args));
        }

        public static void StartHoldingTemp(double Temp)
        {
            ReportDebug("Beginning to hold {0:F2}°C", Temp);
            SendHoldSignal(ConvertFromAmbientTemp(Temp));
        }

        public static void StartRampingTemp(double Ramp, double Target)
        {
            ReportDebug("Marking start time");
            ExperimentStarted = ExperimentRunning = true;
            StartTime = DateTime.Now;
            ReportDebug("Starting ramp of {0:F2}°C to {1:F2}°C", Ramp, Target);
            SendStartSignal(ConvertFromAmbientTemp(Ramp), ConvertFromAmbientTemp(Target));
            //Port.NewLine = END_OF_DATA_BLOCK;
        }

        public static ArduinoStatus Status()
        {
            ReportDebug("Requesting Arduino status");
            SendStatusSignal();
            var msg = ReadMessage();
            var res = (ArduinoStatus) msg[0];
            ReportDebug("Current Arduino status: {0}", Enum.GetName(typeof(ArduinoStatus), res));
            return res;
        }

        private static byte[] ReadBytes(int Count)
        {
            return Enumerable.Range(0, Count)
                .Select(idx => Port.ReadByte())
                .Where(val => val >= 0) // Ignore values given if the port is closed
                .Select(i => (byte)i)
                .ToArray();
        }

        private static ushort ReadChunk()
        {
            ushort val = BitConverter.ToUInt16(ReadBytes(2), 0);
            DataReadLog.Append(string.Format("0x{0:X4} ", val));
            return val;
        }

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

        public static void StopExperiment()
        {
            ReportDebug("Stopping experiment");
            ExperimentRunning = false;
            KeepRunning = false;
            SendStopSignal();
            DeleteExperimentSettingsFile();
        }

        public static void ForceStop()
        {
            ReportDebug("Resetting Arduino to terminate heating");
            ExperimentRunning = false;
            Port.Close();
            Port.Open();
            DeleteExperimentSettingsFile();
        }

        private static void Send(ushort Chunk)
        {
            Port.Write(BitConverter.GetBytes(Chunk), 0, 2);
        }

        private static void Send(ArduinoSignal Sig)
        {
            Send((ushort) Sig);
        }

        private static void SendHoldSignal(ushort Target)
        {
            Send(ArduinoSignal.Hold);
            Send(Target);
        }

        private static void SendStartSignal(ushort Ramp, ushort Target)
        {
            Send(ArduinoSignal.Start);
            Send(Ramp);
            Send(Target);
        }

        private static void SendStopSignal()
        {
            Send(ArduinoSignal.Stop);
        }

        private static void SendStatusSignal()
        {
            Send(ArduinoSignal.Status);
        }

        internal static double ConvertAmbientTemp(int Value)
        {
            return Value * AMBIENT_SCALE + AMBIENT_BASE;
        }

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

        public static void SaveExperimentSettings(string PortName, int NumLizards, int ReportInterval, double StartTemp, double RampTemp, double MaxTemp)
        {
            File.WriteAllLines(RUN_FILE,
                (new object[] { DateTime.Now, PortName, NumLizards, ReportInterval, StartTemp, RampTemp, MaxTemp })
                .Select(obj => obj.ToString()));
        }

        public static bool CheckForRunningExperiment()
        {
            return File.Exists(RUN_FILE);
        }

        public static void DeleteExperimentSettingsFile()
        {
            if (File.Exists(RUN_FILE))
                File.Delete(RUN_FILE);
        }

        public static void GetRunningExperiment(out DateTime SaveTime, out string PortName, out int NumLizards, out int ReportInterval, out double StartTemp, out double RampTemp, out double MaxTemp)
        {
            string[] Lines = File.ReadAllLines(RUN_FILE);
            SaveTime = DateTime.Parse(Lines[0]);
            PortName = Lines[1];
            NumLizards = int.Parse(Lines[2]);
            ReportInterval = int.Parse(Lines[3]);
            StartTemp = double.Parse(Lines[4]);
            RampTemp = double.Parse(Lines[5]);
            MaxTemp = double.Parse(Lines[6]);
        }

        public static void ResyncWithArduino()
        {
            var status = Status();
            switch(status)
            {
                case ArduinoStatus.None:
                    return;
                case ArduinoStatus.Preparing:
                    return;
                case ArduinoStatus.Ramping:
                    ExperimentStarted = ExperimentRunning = true;
                    break;
            }
        }
    }
}
