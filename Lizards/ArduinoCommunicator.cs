using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Lizards
{
    public static class ArduinoCommunicator
    {
        public const int BAUD_RATE = 9600;
        public const Parity PARITY = Parity.Even;
        public static readonly StopBits STOP_BITS = StopBits.One;
        public const int BITS_PER_DATA = 8;
        //public const string END_OF_DATA_BLOCK = "";

        public const double AMBIENT_SCALE = 1/16d, AMBIENT_BASE = 0d;
        public const double LIZARD_SCALE = 1/16d, LIZARD_BASE = 0d;

        private static SerialPort Port;
        private static bool KeepRunning;
        public static LizardData[] Lizards { get; private set; }

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

        public static void StartHoldingTemp()
        {
            SendHoldSignal();
        }

        public static void StartRampingTemp()
        {
            SendStartSignal();
            //Port.NewLine = END_OF_DATA_BLOCK;
            KeepRunning = true;
            Task ListenToArduino = new Task(() =>
            {
                while (KeepRunning)
                {
                    double[] temps = ReadTemps();
                    LizardData.CurrentAmbientTemp = temps[0];
                    temps = temps.Skip(1).ToArray(); // Drop the first value, since we've used it already
                    foreach(var liz_temp in Lizards.Zip(temps, (liz, temp) => new Tuple<LizardData, double>(liz, temp)))
                        liz_temp.Item1.Update(liz_temp.Item2);
                }
            });
            ListenToArduino.Start();
        }

        private static byte[] ReadBytes(int Count)
        {
            return Enumerable.Range(0, Count)
                .Select(idx => Port.ReadByte())
                .Where(val => val > 0) // Ignore values given if the port is closed
                .Select(i => (byte)i)
                .ToArray();
        }

        private static double[] ReadTemps()
        {
            double ambient = ConvertAmbientTemp(BitConverter.ToUInt16(ReadBytes(2), 0));
            List<double> lizard_temps =
                Lizards.Select(junk => ConvertLizardTemp(BitConverter.ToUInt16(ReadBytes(2), 0))).ToList();
            lizard_temps.Insert(0, ambient);
            return lizard_temps.ToArray();
        }

        public static void StopExperiment()
        {
            KeepRunning = false;
            SendStopSignal();
        }

        private static void SendHoldSignal()
        {
            
        }

        private static void SendStartSignal()
        {
            
        }

        private static void SendStopSignal()
        {
            
        }

        private static double ConvertAmbientTemp(int Value)
        {
            return Value * AMBIENT_SCALE + AMBIENT_BASE;
        }

        private static double ConvertLizardTemp(int Value)
        {
            return Value * LIZARD_SCALE + LIZARD_BASE;
        }
    }
}
