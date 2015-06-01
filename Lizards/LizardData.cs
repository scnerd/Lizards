using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Lizards
{
    public delegate void NewLizardDataHandler(LizardData newData, double newTemp);

    public class LizardData
    {
        public static string[] EVENTS = { "Panting", "LRR", "Spasming (stop)" };
        public static int NUM_EVENTS = EVENTS.Length;

        public class Record
        {
            public DateTime Timestamp;
            public double LizardTemp;
            public double AmbientTemp;
            public string Note;

            public Record(LizardData Owner, string Note = null)
            {
                Timestamp = DateTime.Now;
                LizardTemp = Owner.CurrentLizardTemp;
                AmbientTemp = CurrentAmbientTemp;
                this.Note = Note;
            }
        }

        public event NewLizardDataHandler OnNewData;

        public int Number { get; private set; }
        public List<double> Temperatures = new List<double>();
        public double CurrentLizardTemp { get { return Temperatures.Last(); } }
        public static double CurrentAmbientTemp = double.NaN;

        public bool IsActive { get; private set; }

        public Record[] MainEvents = new Record[NUM_EVENTS]; 
        public List<Record> Notes = new List<Record>();
        public List<Record> TempRecords = new List<Record>();
        public readonly object AllNotesLock = new object();

        public IEnumerable<Record> ImportantRecords
        {
            get
            {
                lock (AllNotesLock)
                {
                    return MainEvents.Concat(Notes).Where(rec => rec != null).OrderBy(rec => rec.Timestamp);
                }
            }
        }

        public LizardData(int LizardNumber)
        {
            Number = LizardNumber;
            IsActive = true;
        }

        internal void Update(double Temperature)
        {
            if (IsActive)
            {
                Temperatures.Add(Temperature);
                TempRecords.Add(new Record(this));
                if (OnNewData != null)
                    OnNewData(this, Temperature);
            }
        }

        public void Stop()
        {
            IsActive = false;
        }
    }
}
