using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace Lizards
{
    public delegate void NewAmbientTempHandler(double newTemp);

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
        //public const string END_OF_DATA_BLOCK = "";

        public const double AMBIENT_SCALE = 1/16d, AMBIENT_BASE = 0d;
        public const double LIZARD_SCALE = 1/16d, LIZARD_BASE = 0d;

        private static SerialPort Port;
        private static bool KeepRunning;
        private static bool AlreadyWrote = false;
        public static LizardData[] Lizards { get; private set; }

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

                try
                {
                    SaveResults(false);
                }
                catch (Exception)
                {
                }
            });
        }

        public static string[] GetPossiblePorts()
        {
            return SerialPort.GetPortNames();
        }

        public static void Connect(string ToConnect)
        {
            // Prepare settings to connect to Arduino
            Port = new SerialPort(ToConnect, BAUD_RATE, PARITY);
            Port.StopBits = STOP_BITS;
            Port.DataBits = BITS_PER_DATA;
            // Resets the Arduino upon connecting
            Port.DtrEnable = true;
            // Open the port (throws System.UnauthorizedAccessException if the port is already open elsewhere)
            Port.Open();
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

        public static void StartHoldingTemp(double Temp)
        {
            SendHoldSignal(ConvertFromAmbientTemp(Temp));
        }

        public static void StartRampingTemp(double Ramp, double Target)
        {
            SendStartSignal(ConvertFromAmbientTemp(Ramp), ConvertFromAmbientTemp(Target));
            //Port.NewLine = END_OF_DATA_BLOCK;
            KeepRunning = true;
            Task ListenToArduino = new Task(() =>
            {
                while (KeepRunning)
                {
                    double[] temps = ReadTemps();
                    OnNewAmbientTemp(temps[0]);
                    LizardData.CurrentAmbientTemp = temps[0];
                    temps = temps.Skip(1).ToArray(); // Drop the first value, since we've used it already
                    foreach(var liz_temp in Lizards.Zip(temps, (liz, temp) => new Tuple<LizardData, double>(liz, temp)))
                        liz_temp.Item1.Update(liz_temp.Item2);
                }
            });
            ListenToArduino.Start();
        }

        public static ArduinoStatus Status()
        {
            SendStatusSignal();
            var msg = ReadMessage();
            return (ArduinoStatus) msg[0];
        }

        private static byte[] ReadBytes(int Count)
        {
            return Enumerable.Range(0, Count)
                .Select(idx => Port.ReadByte())
                .Where(val => val > 0) // Ignore values given if the port is closed
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
                Debug.WriteLine("Failed to read temperatures: Wrong number of temperatures read from Arduino");
                DumpReadData();
            }
            return lizard_temps.ToArray();
        }

        public static void StopExperiment()
        {
            KeepRunning = false;
            SendStopSignal();
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

        private static double ConvertAmbientTemp(int Value)
        {
            return Value * AMBIENT_SCALE + AMBIENT_BASE;
        }

        private static ushort ConvertFromAmbientTemp(double Value)
        {
            return (ushort) Math.Round((Value - AMBIENT_BASE)/AMBIENT_SCALE);
        }

        private static double ConvertLizardTemp(int Value)
        {
            return Value * LIZARD_SCALE + LIZARD_BASE;
        }

        public static string Combine(this string Base, params string[] Others)
        {
            return Others.Aggregate((a, b) => a + Base + b);
        }

        public static string SaveResults(bool WriteEvenIfAlreadyWrote = true)
        {
            if (AlreadyWrote && !WriteEvenIfAlreadyWrote)
                return null;

            string path = string.Format("Lizard Results {0:u}.csv", DateTime.UtcNow).Replace(":", "_");
            //using (StreamWriter file = File.CreateText(path)) // Need to make sure you don't overwrite existing file
            //{
                // Write out csv of results
            //}
            return path;
        }

        private static void DumpReadData()
        {
            Debug.WriteLine("Data read log:");
            Debug.WriteLine(DataReadLog.ToString());
            DataReadLog = new StringBuilder();
        }
    }
}
