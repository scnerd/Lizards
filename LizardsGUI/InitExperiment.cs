using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lizards;

////PICK UP RUNNING EXPERIMENT
namespace LizardsGUI
{
    public partial class InitExperiment : Form
    {
        public int NumLizards;
        public int ReportInterval;
        public string ArduinoPort;
        public double HoldTemp, RampTemp, MaxTemp, HeaterTemp;

        private int defaultNumLizards, defaultReportInterval;
        private decimal defaultHold, defaultRamp, defaultMax, defaultHeater;

        public InitExperiment()
        {
            InitializeComponent();
            defaultNumLizards = (int) numNumLizards.Value;
            defaultReportInterval = (int)numReport.Value;
            defaultHold = numHoldTemp.Value;
            defaultRamp = numRampTemp.Value;
            defaultMax = numMaxTemp.Value;
            defaultHeater = numHeater.Value;
            ResetComPorts();
        }

        private void InitExperiment_FormClosing(object sender, FormClosingEventArgs e)
        {
            NumLizards = (int)numNumLizards.Value;
            ReportInterval = (int)numReport.Value;
            HoldTemp = (double)numHoldTemp.Value;
            RampTemp = (double)numRampTemp.Value;
            MaxTemp = (double)numMaxTemp.Value;
            HeaterTemp = (double)numHeater.Value;
            // This just drops an empty port name if there is no port selected (only happens when no ports are available, in which case we're closing anyway)
            ArduinoPort = (cmbComPorts.SelectedItem ?? "").ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ResetComPorts()
        {
            bool insuranceCheck = true;
            while (insuranceCheck)
            {
                cmbComPorts.Items.Clear();
                cmbComPorts.Items.AddRange(ArduinoCommunicator.GetPossiblePorts());
                try
                {
                    cmbComPorts.SelectedIndex = 0;
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (
                        MessageBox.Show(
                            "Please plug in your Arduino and ensure that it has a COM port (look under 'Ports' in Device Manager)",
                            "Error: No Arduino Found",
                            MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        Application.Exit();
                    }
                    else
                    {
                        continue;
                    }
                }
                insuranceCheck = false;
                break;
            }
        }

        private void btnComPorts_Click(object sender, EventArgs e)
        {
            ResetComPorts();
        }

        private void btnNumLizards_Click(object sender, EventArgs e)
        {
            numNumLizards.Value = defaultNumLizards;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            numReport.Value = defaultReportInterval;
        }

        private void btnHoldTemp_Click(object sender, EventArgs e)
        {
            numHoldTemp.Value = defaultHold;
        }

        private void btnRampTemp_Click(object sender, EventArgs e)
        {
            numRampTemp.Value = defaultRamp;
        }

        private void btnMaxTemp_Click(object sender, EventArgs e)
        {
            numMaxTemp.Value = defaultMax;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numHeater.Value = defaultHeater;
        }
    }
}
