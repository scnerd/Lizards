using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lizards;

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
            init.ShowDialog();

            int NumLizards = init.NumLizards;
            ArduinoCommunicator.InitializeLizards(NumLizards);
            tblLizards.SuspendLayout();
            tblLizards.RowStyles.Clear();
            for (int i = 0; i < NumLizards; i++)
            {
                LizardMonitor mon = new LizardMonitor();
                mon.Lizard = ArduinoCommunicator.Lizards[i];
                mon.Dock = DockStyle.Fill;
                tblLizards.Controls.Add(mon);
            }
            tblLizards.ResumeLayout();
        }
    }
}
