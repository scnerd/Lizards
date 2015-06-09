using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lizards;
using System.IO;

/*
 * Buttons to send change of state signals
 * Offer hard reset by forcing arduino reset (recommend unplugging power also)
 */
namespace LizardsGUI
{
    public partial class MainForm : Form
    {
        // Stores the variables from initialization
        private int NumLizards;
        private int ReportInterval;
        private string PortName;
        private double HoldTemp;
        private double RampTemp;
        private double MaxTemp;
        private double HeaterTemp;

        public MainForm()
        {
            InitializeComponent();
            ArduinoCommunicator.AddDebugOutput(new TextboxWriter(txtDebugLog));
            gphEnvironTemp.Clear();

            gphEnvironTemp.Text = "Lizard Environment Temp";
            gphHeaterTemp.Text = "Heating Chamber Temp";
        }

        /// <summary>
        /// While loading, initialize the experiment and construct UI components for each lizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Launch the init form, closing the program if is cancelled
            InitExperiment init = new InitExperiment();
            if (init.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
                return;
            }

            // Save the initialization values
            NumLizards = init.NumLizards;
            ReportInterval = init.ReportInterval;
            PortName = init.ArduinoPort;
            HoldTemp = init.HoldTemp;
            RampTemp = init.RampTemp;
            MaxTemp = init.MaxTemp;

            // Set up the serial communication
            ArduinoCommunicator.InitializeLizards(NumLizards);
            ArduinoCommunicator.OnNewEnvironmentTemp += ArduinoCommunicator_OnNewEnvironTemp;
            ArduinoCommunicator.OnNewHeaterTemp += ArduinoCommunicator_OnNewHeaterTemp;

            // Create the UI elements for each lizard
            tblLizards.SuspendLayout();
            tblLizards.RowStyles.Clear();
            for (int i = 0; i < NumLizards; i++)
            {
                LizardMonitor mon = new LizardMonitor();
                mon.Lizard = ArduinoCommunicator.Lizards[i];
                mon.Dock = DockStyle.Fill;
                tblLizards.Controls.Add(mon);
                tblLizards.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
            }
            tblLizards.ResumeLayout();

            // Begin talking to the Arduino
            ArduinoCommunicator.Connect(PortName);
        }

        /// <summary>
        /// When a new heating chamber temperature is available, display it
        /// </summary>
        /// <param name="newTemp">The new temperature to display</param>

        private void ArduinoCommunicator_OnNewHeaterTemp(double newTemp)
        {
            gphHeaterTemp.Add(newTemp);
            lblHeaterTemp.Text = string.Format("Heater Temp: {0:F2}°C", newTemp);
        }

        /// <summary>
        /// When a new ambient temperature is available, display it
        /// </summary>
        /// <param name="newTemp">The new temperature to display</param>
        private void ArduinoCommunicator_OnNewEnvironTemp(double newTemp)
        {
            gphEnvironTemp.Add(newTemp);
            lblEnvironTemp.Text = string.Format("Environment Temp: {0:F2}°C", newTemp);
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            ArduinoCommunicator.StartHoldingTemp(HoldTemp, HeaterTemp);
        }

        private void btnRamp_Click(object sender, EventArgs e)
        {
            ArduinoCommunicator.StartRampingTemp(RampTemp, MaxTemp);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ArduinoCommunicator.StopExperiment();
            ArduinoCommunicator.SaveResults(ReportInterval, true);
        }

        private void btnForceStop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset the Arduino and disable the heater, are you sure?", "Confirm reset", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                ArduinoCommunicator.ForceStop();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArduinoCommunicator.SaveResults(ReportInterval, true);
        }

        /// <summary>
        /// Captures keyboard presses to save the data when CTRL+S is pressed
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                btnSave.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    /// <summary>
    /// Defines a wrapper around the textbox to allow it to be written to serially via a TextWriter (used for debug output)
    /// </summary>
    internal class TextboxWriter : TextWriter
    {
        public TextBox Output { get; private set; }

        public TextboxWriter(TextBox box)
        {
            Output = box;
        }

        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        public override void Write(char value)
        {
            Output.BeginInvoke(new Action(() =>
            {
                lock (Output)
                {
                    if (!Output.IsDisposed)
                        Output.AppendText(value.ToString());
                }
            }));
        }
    }
}
