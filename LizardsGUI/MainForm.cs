using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lizards;

/*
 * Buttons to send change of state signals
 * Offer hard reset by forcing arduino reset (recommend unplugging power also)
 */
namespace LizardsGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitExperiment init = new InitExperiment();
            if (init.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
                return;
            }

            int NumLizards = init.NumLizards;
            int ReportInterval = init.ReportInterval;
            string PortName = init.ArduinoPort;
            double HoldTemp = init.HoldTemp;
            double RampTemp = init.RampTemp;
            double MaxTemp = init.MaxTemp;

            ArduinoCommunicator.Connect(PortName);
            ArduinoCommunicator.InitializeLizards(NumLizards);
            //if (ArduinoCommunicator.Status() != ArduinoCommunicator.ArduinoStatus.None)
            //    MessageBox.Show("An experiement is already being performed, resuming is not yet implemented",
            //        "Notice of running experiment");
            // TODO: Implement resuming a running experiment

            tblLizards.SuspendLayout();
            tblLizards.RowStyles.Clear();
            for (int i = 0; i < NumLizards; i++)
            {
                LizardMonitor mon = new LizardMonitor();
                mon.Lizard = ArduinoCommunicator.Lizards[i];
                mon.Dock = DockStyle.Fill;
                tblLizards.Controls.Add(mon);
                tblLizards.RowStyles.Add(new RowStyle(SizeType.Percent, 1f));
            }
            tblLizards.ResumeLayout();

            ArduinoCommunicator.OnNewAmbientTemp +=ArduinoCommunicator_OnNewAmbientTemp;
        }

        private void ArduinoCommunicator_OnNewAmbientTemp(double newTemp)
        {
            gphAmbientTemp.Add(newTemp);
            lblAmbientTemp.Text = string.Format("Ambient Temp: {0:F2}°C", newTemp);
        }

        private void btnHold_Click(object sender, EventArgs e)
        {

        }

    }
}
