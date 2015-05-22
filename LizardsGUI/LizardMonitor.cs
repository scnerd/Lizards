using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lizards;

namespace LizardsGUI
{
    public partial class LizardMonitor : UserControl
    {
        public const double MIN_TEMP = 20d;
        public const double MAX_TEMP = 55d;

        private LizardData _Lizard;

        public LizardMonitor()
        {
            InitializeComponent();
            gphTempGraph.Clear();

            demo_junk();
        }

        public LizardData Lizard
        {
            get { return _Lizard; }
            set
            {
                _Lizard = value;
                _Lizard.OnNewData += new NewLizardDataHandler(this.OnNewData);
                lblLizardName.Text = string.Format("Lizard {0}", value.Number + 1);
            }
        }

        public void OnNewData(LizardData sender, double NewTemp)
        {
            gphTempGraph.Add((NewTemp - MIN_TEMP) / (MAX_TEMP - MIN_TEMP));
            lblCurrentTemp.Text = string.Format("Temp: {0}°C", NewTemp);
        }

        private void demo_junk()
        {
            var timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        private Random rnd = new Random();
        private double d = 0d;
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool Up = rnd.NextDouble() > d;
            d += rnd.NextDouble() * 0.1d * (Up ? 1 : -1);
            gphTempGraph.Add(d);
        }

        private void btnEvent1_Click(object sender, EventArgs e)
        {
            dataRecords.Rows.Add(DateTime.Now, "Yo");
        }
    }
}
