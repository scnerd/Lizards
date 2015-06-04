using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Lizards;
using TextMenu;
using System.Timers;

namespace LizardsConsole
{
    class Program
    {
        public Menu MainMenu, LizardsMenu, ControlMenu;
        public const double MIN_TEMP = 20d;
        public const double MAX_TEMP = 55d;

        public int NumLizards;
        public int ReportInterval;
        public string ArduinoPort;
        public double HoldTemp, RampTemp, MaxTemp;

        public Program()
        {
            MainMenu = new Menu(Console.Out, Console.In, false, true);
            
            // Initialization
            NumLizards = MainMenu.RequestInt("Number of lizards (default: {0}) (max: 9): ", Default: 5).Value;
            ReportInterval = MainMenu.RequestInt("Report interval in seconds (default: {0}): ", Default: 15).Value;
            int? ArduinoPortChoice = null;
            while (!ArduinoPortChoice.HasValue)
            {
                var Ports = ArduinoCommunicator.GetPossiblePorts();
                if(Ports != null && Ports.Length < 1)
                {
                    MainMenu.OutStream.WriteLine("No possible ports found, please plug in the Arduino and hit ENTER to retry");
                    MainMenu.RequestString("");
                    continue;
                }
                string PortChoices =
                    ", ".Combine(
                        Ports
                            .Select((port, idx) => string.Format("{0}: {1}", idx, port))
                            .ToArray());
                ArduinoPortChoice =
                    MainMenu.RequestInt(string.Format("Arduino COM port (possible options are {0}): ", PortChoices),
                        Retry: false);
                if (ArduinoPortChoice.HasValue)
                    ArduinoPort = Ports[ArduinoPortChoice.Value];
            }
            HoldTemp = MainMenu.RequestDouble("Hold temperature in C (default: {0}): ", Default: 27).Value;
            RampTemp = MainMenu.RequestDouble("Ramp temperature in C/min (default: {0}): ", Default: 1).Value;
            MaxTemp = MainMenu.RequestDouble("Max temperature in C (default: {0}): ", Default: 50).Value;

            InitializeMenus();
        }

        private void InitializeMenus()
        {
            LizardsMenu = new Menu(Console.Out, Console.In, false, true);
            ControlMenu = new Menu(Console.Out, Console.In, false, true);

            MainMenu.AddItem(new MenuItem('T', "Show temperatures overview", _Temperatures));
            MainMenu.AddItem(new MenuItem('S', "Save data", _SaveData));
            MainMenu.AddItem(new MenuItem('L', "Show lizards menu", () => LizardsMenu.Run("--- Lizards Menu ---", ReprintOnFail: true)));
            MainMenu.AddItem(new MenuItem('C', "Show controls menu", () => ControlMenu.Run("--- Control Menu ---", ReprintOnFail: true)));
            MainMenu.AddItem(new MenuItem('H', "Help", _MainMenuHelp));
            MainMenu.AddItem(new MenuItem('Q', "Save and quit", MainMenu.CloseSelf));

            // TODO: Add resilience above 9 lizards
            var CreateClosure = new Func<int, Action>((j) => new Action(() => _ShowLizData(j)));
            for(int i = 0; i < NumLizards; i++)
                LizardsMenu.AddItem(new MenuItem((i+1).ToString()[0], string.Format("Lizard {0}", i + 1), CreateClosure(i)));
            LizardsMenu.AddItem(new MenuItem('H', "Help", _LizardsHelp));
            LizardsMenu.AddItem(new MenuItem('Q', "Back", LizardsMenu.CloseSelf));

            ControlMenu.AddItem(new MenuItem('1', "Begin holding start temperature", _HoldTemp));
            ControlMenu.AddItem(new MenuItem('2', "Begin ramping heater", _StartRamp));
            ControlMenu.AddItem(new MenuItem('3', "Stop ramping heater", _StopRamp));
            ControlMenu.AddItem(new MenuItem('X', "Emergency stop heater (recommend unplugging as well)", _ForceStop));
            //ControlMenu.AddItem(new MenuItem('S', "Save data", _SaveData)); // Already available in the main menu
            ControlMenu.AddItem(new MenuItem('H', "Help", _ControlHelp));
            ControlMenu.AddItem(new MenuItem('Q', "Back", ControlMenu.CloseSelf));
        }

        private void _MainMenuHelp()
        {
            MainMenu.PrintLine("The MAIN MENU provides the starting point for all controls");
            MainMenu.PrintLine("To begin the experiment or otherwise control the Arduino, see the controls menu");
            MainMenu.PrintLine("To mark events on lizards, use the lizards menu");
        }

        private void _LizardsHelp()
        {
            LizardsMenu.PrintLine("The LIZARD MENU provides methods for marking events on individual lizards");
            LizardsMenu.PrintLine("Select a lizard's number to mark events or add notes to that lizard");
        }

        private void _ControlHelp()
        {
            ControlMenu.PrintLine("The CONTROL MENU allows sending signals to the Arduino");
            ControlMenu.PrintLine("Use the commands in order for a regular experiment");
            ControlMenu.PrintLine("The emergency stop will reset the Arduino to force it to stop its heating ramp");
            ControlMenu.PrintLine("It is recommended not to just close the control program if you're experiencing difficulties, as the stop signal may not be handled correctly");
        }

        private void _Temperatures()
        {
            // Each half second, ...
            Console.Clear();
            while(!Console.KeyAvailable)
            {
                // ... print into the same spot ...
                Console.SetCursorPosition(0, 0);
                int bar_size = Console.WindowWidth - 21;
                double percent;
                // ... bars indicating the current temperature for each lizard, with headers above them ...
                Console.WriteLine(string.Format(@"{{0,{0}}}C{{1,{1}}}C", 12, Console.WindowWidth - 12 - 9), MIN_TEMP, MAX_TEMP);
                foreach (LizardData liz in ArduinoCommunicator.Lizards)
                {
                    percent = (liz.CurrentLizardTemp - MIN_TEMP) / (MAX_TEMP - MIN_TEMP);
                    Console.WriteLine(string.Format("Lizard {0,2}: [{{0,-{1}}}] {2:F2}C", liz.Number + 1, bar_size, liz.CurrentLizardTemp), string.Concat(Enumerable.Repeat("-", (int)(percent * bar_size))));
                }
                // ... and the same thing for the ambient temperature ...
                Console.WriteLine(string.Format(@"{{0,{0}}}C{{1,{1}}}C", 12, Console.WindowWidth - 12 - 9), HoldTemp, MaxTemp);
                percent = (LizardData.CurrentAmbientTemp - HoldTemp) / (MaxTemp - HoldTemp);
                Console.WriteLine(string.Format("Ambient :  [{{0,-{0}}}] {1:F2}C", bar_size, LizardData.CurrentAmbientTemp), string.Concat(Enumerable.Repeat("-", (int)(percent * bar_size))));
                Console.WriteLine();
                Console.WriteLine("Press any key to return...");
                System.Threading.Thread.Sleep(500);
                // ... stopping when any key is pressed
            }
            Console.Clear();
            Console.ReadKey(); // clear the key that was pressed
        }

        private void _SaveData()
        {
            MainMenu.PrintLine("Data saving");
            MainMenu.PrintLine(ArduinoCommunicator.SaveResults(ReportInterval, true) ?? "Data not saved, experiment is probably not running yet");
        }

        private void _Quit()
        {
            _SaveData();
        }

        private void _ShowLizData(int LizNum)
        {
            LizardData Liz = ArduinoCommunicator.Lizards[LizNum];
            Menu CurrentLizardMenu = new Menu(Console.Out, Console.In, false, true);
            Action DisplayRecords = () =>
            {
                CurrentLizardMenu.PrintLine("Lizard {0}", Liz.Number + 1);
                CurrentLizardMenu.PrintLine("Temperature: {0}°C", Liz.CurrentLizardTemp);
                foreach (LizardData.Record rec in Liz.ImportantRecords)
                {
                    CurrentLizardMenu.PrintLine("{0:H:mm:ss} at {1}°C (ambient of {2}°C): {3}", rec.Timestamp, rec.LizardTemp,
                        rec.AmbientTemp, rec.Note);
                }
                CurrentLizardMenu.PrintLine();
            };

            CurrentLizardMenu.AddItem('1', Constants.LIZARD_EVENTS[0], () => Liz.MainEvents[0] = new LizardData.Record(Liz, Constants.LIZARD_EVENTS[0]));
            CurrentLizardMenu.AddItem('2', Constants.LIZARD_EVENTS[1], () => Liz.MainEvents[1] = new LizardData.Record(Liz, Constants.LIZARD_EVENTS[1]));
            CurrentLizardMenu.AddItem('3', Constants.LIZARD_EVENTS[2], () =>
            {
                Liz.MainEvents[2] = new LizardData.Record(Liz, Constants.LIZARD_EVENTS[2]);
                Liz.Stop();
            });
            CurrentLizardMenu.AddItem('N', "Make note", () => Liz.Notes.Add(new LizardData.Record(Liz, CurrentLizardMenu.RequestString("Note text: "))));
            CurrentLizardMenu.AddItem('D', "Display records", DisplayRecords);
            CurrentLizardMenu.AddItem('Q', "Back", CurrentLizardMenu.CloseSelf);
            CurrentLizardMenu.Run(string.Format("--- Lizard {0} ---", LizNum + 1), ReprintOnFail: true);
        }

        private void _HoldTemp()
        {
            ArduinoCommunicator.StartHoldingTemp(HoldTemp);
        }

        private void _StartRamp()
        {
            ArduinoCommunicator.StartRampingTemp(RampTemp, MaxTemp);
        }

        private void _StopRamp()
        {
            ArduinoCommunicator.StopExperiment();
        }

        private void _ForceStop()
        {
            ArduinoCommunicator.ForceStop();
        }

        static void Main(string[] args)
        {
            Program prog = new Program();
            ArduinoCommunicator.InitializeLizards(prog.NumLizards);
            ArduinoCommunicator.Connect(prog.ArduinoPort);

            prog.MainMenu.Run("--- Main Menu ---", ReprintOnFail: true);

            prog._Quit();
        }
    }
}
