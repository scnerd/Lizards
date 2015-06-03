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
        private int NumLizards;
        private int ReportInterval;
        private string PortName;
        private double HoldTemp;
        private double RampTemp;
        private double MaxTemp;

        public MainForm()
        {
            InitializeComponent();
            ArduinoCommunicator.AddDebugOutput(new TextboxWriter(txtDebugLog));
            gphAmbientTemp.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
                InitExperiment init = new InitExperiment();
                if (init.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }

                NumLizards = init.NumLizards;
                ReportInterval = init.ReportInterval;
                PortName = init.ArduinoPort;
                HoldTemp = init.HoldTemp;
                RampTemp = init.RampTemp;
                MaxTemp = init.MaxTemp;

            ArduinoCommunicator.InitializeLizards(NumLizards);
            ArduinoCommunicator.Connect(PortName);

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

            ArduinoCommunicator.OnNewAmbientTemp += ArduinoCommunicator_OnNewAmbientTemp;
        }

        private void ArduinoCommunicator_OnNewAmbientTemp(double newTemp)
        {
            gphAmbientTemp.Add(newTemp);
            lblAmbientTemp.Text = string.Format("Ambient Temp: {0:F2}°C", newTemp);
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            ArduinoCommunicator.StartHoldingTemp(HoldTemp);
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
            if(MessageBox.Show("This will reset the Arduino and disable the heater, are you sure?", "Confirm reset", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                ArduinoCommunicator.ForceStop();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArduinoCommunicator.SaveResults(ReportInterval, true);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == (Keys.Control | Keys.S))
            {
                btnSave.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

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
            if(!Output.IsDisposed)
                Output.Invoke(new Action(() => Output.AppendText(value.ToString())));
        }
    }
}
