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

        private LizardData _Lizard;

        public LizardMonitor()
        {
            InitializeComponent();
            gphTempGraph.Clear();

            // Make sure that all LizardMonitors resize when any of them resizes
            OuterResized += OnOuterResized;
            InnerResized += OnInnerResized;

            // Set the button text to what it's supposed to be, as defined in Constants
            btnEvent1.Text = Constants.LIZARD_EVENTS[0];
            btnEvent2.Text = Constants.LIZARD_EVENTS[1];
            btnStop.Text = Constants.LIZARD_EVENTS[2];
        }

        public LizardData Lizard
        {
            get { return _Lizard; }
            set
            {
                if(_Lizard != null)
                    _Lizard.OnNewData -= this.OnNewData;
                // Set up the control to properly display the Lizard's name and report its temperatures
                _Lizard = value;
                _Lizard.OnNewData += this.OnNewData;
                lblLizardName.Text = string.Format("Lizard {0}", value.Number + 1);
            }
        }

        public void OnNewData(LizardData sender, double NewTemp)
        {
            // Put the new temperature in its label and on the graph, if we're still tracking temperatures
            if (_Lizard.IsActive)
            {
                gphTempGraph.Add(NewTemp);
                this.BeginInvoke(new Action(() =>
                {
                    lock (lblCurrentTemp)
                    {
                        if (!lblCurrentTemp.IsDisposed)
                            lblCurrentTemp.Text = string.Format("Temp: {0:F2}°C", NewTemp);
                    }
                }));
            }
        }

        /// <summary>
        /// Generic handler for the multiple event buttons
        /// </summary>
        /// <param name="ToDisable">The button that should get gray'd out</param>
        /// <param name="ToEnable">The button that should get enabled</param>
        /// <param name="EventIndex">The event number to mark in the lizard</param>
        private void Event(Button ToDisable, Button ToEnable, int EventIndex)
        {
            if (ToDisable != null) ToDisable.Enabled = false;
            if (ToEnable != null) ToEnable.Enabled = true;
            lock (_Lizard.AllNotesLock)
            {
                _Lizard.MainEvents[EventIndex] = new LizardData.Record(_Lizard, Constants.LIZARD_EVENTS[EventIndex]);
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
            // Create the new record, and focus on its editable cell
            _Lizard.Notes.Add(new LizardData.Record(_Lizard));
            UpdateNotesTable();
            dataRecords.CurrentCell = dataRecords["Note", dataRecords.Rows.Count - 1];
            dataRecords.Select();
        }

        private void dataRecords_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Find the record corresponding to the cell that was edited and change it accordingly
            lock (_Lizard.AllNotesLock)
            {
                _Lizard.ImportantRecords.ElementAt(e.RowIndex).Note = dataRecords[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
            UpdateNotesTable();
        }

        private void sptInner_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // Resize all others when this control's inner panel is resized
            InnerResized(this, sptInner.SplitterDistance);
        }

        private void sptOuter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // Resize all others when this control's outer panel is resized
            OuterResized(this, sptOuter.SplitterDistance);
        }

        private void OnInnerResized(LizardMonitor sender, int NewPos)
        {
            // Resize this when another's inner panel was resized
            if (sender != this)
                sptInner.SplitterDistance = NewPos;
        }

        private void OnOuterResized(LizardMonitor sender, int NewPos)
        {
            // Resize this when another's outer panel was resized
            if (sender != this)
                sptOuter.SplitterDistance = NewPos;
        }

        /// <summary>
        /// Outputs on the DataGridView the data that's in the lizard's records
        /// </summary>
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
