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
        protected delegate void StaticResizeHandler(LizardMonitor sender, int NewPos);

        protected static event StaticResizeHandler OuterResized;
        protected static event StaticResizeHandler InnerResized;

        public const double MIN_TEMP = 20d;
        public const double MAX_TEMP = 55d;
        public readonly string[] EVENTS = {"Event 1", "Event 2", "Event 3"};

        private LizardData _Lizard;

        public LizardMonitor()
        {
            InitializeComponent();
            gphTempGraph.UpperLimit = MAX_TEMP;
            gphTempGraph.LowerLimit = MIN_TEMP;
            gphTempGraph.Clear();
            OuterResized += OnOuterResized;
            InnerResized += OnInnerResized;
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
            if (_Lizard.IsActive)
            {
                gphTempGraph.Add(NewTemp);
                this.Invoke(new Action(() => lblCurrentTemp.Text = string.Format("Temp: {0:F2}°C", NewTemp)));
            }
        }

        private void Event(Button ToDisable, Button ToEnable, int EventIndex)
        {
            if (ToDisable != null) ToDisable.Enabled = false;
            if (ToEnable != null) ToEnable.Enabled = true;
            lock (_Lizard.AllNotesLock)
            {
                _Lizard.MainEvents[EventIndex] = new LizardData.Record(_Lizard, EVENTS[EventIndex]);
            }
            UpdateNotesTable();
        }

        private void btnEvent1_Click(object sender, EventArgs e)
        {
            Event(btnEvent1, btnEvent2, 0);
        }

        private void btnEvent2_Click(object sender, EventArgs e)
        {
            Event(btnEvent2, btnStop, 1);
        }

        private void btnEvent3_Click(object sender, EventArgs e)
        {
            Event(btnStop, null, 2);
            _Lizard.Stop();
        }

        private void btnNewNote_Click(object sender, EventArgs e)
        {
            _Lizard.Notes.Add(new LizardData.Record(_Lizard));
            UpdateNotesTable();
        }

        private void dataRecords_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            lock (_Lizard.AllNotesLock)
            {
                _Lizard.ImportantRecords.ElementAt(e.RowIndex).Note = dataRecords[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
            UpdateNotesTable();
        }

        private void sptInner_SplitterMoved(object sender, SplitterEventArgs e)
        {
            InnerResized(this, sptInner.SplitterDistance);
        }

        private void sptOuter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            OuterResized(this, sptOuter.SplitterDistance);
        }

        private void OnInnerResized(LizardMonitor sender, int NewPos)
        {
            if (sender != this)
                sptInner.SplitterDistance = NewPos;
        }

        private void OnOuterResized(LizardMonitor sender, int NewPos)
        {
            if (sender != this)
                sptOuter.SplitterDistance = NewPos;
        }

        private void UpdateNotesTable()
        {
            dataRecords.Rows.Clear();
            lock (_Lizard.AllNotesLock)
            {
                foreach (LizardData.Record rec in _Lizard.ImportantRecords)
                {
                    if (rec != null)
                        dataRecords.Rows.Add(rec.Timestamp, rec.AmbientTemp, rec.LizardTemp, rec.Note);
                }
            }
        }
    }
}
