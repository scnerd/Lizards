using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Lizards
{
    public delegate void NewLizardDataHandler(LizardData newData, double newTemp);

    /// <summary>
    /// Tracks data and events for a single lizard
    /// </summary>
    public class LizardData
    {

        /// <summary>
        /// Tracks the time, temperatures, and note for a given note or event for a lizard
        /// </summary>
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
                AmbientTemp = CurrentEnvironmentTemp;
                this.Note = Note;
            }
        }

        // This event fires when a new lizard temperature arrives from the Arduino
        public event NewLizardDataHandler OnNewData;

        // The ID number of this lizard
        public int Number { get; private set; }

        // A record of the temperatures of this lizard (maybe could get optimized out)
        //public List<double> Temperatures = new List<double>();
        //public double CurrentLizardTemp { get { return Temperatures.Last(); } }
        public double CurrentLizardTemp { get; private set; }
        public static double CurrentEnvironmentTemp = double.NaN;
        public static double CurrentHeaterTemp = double.NaN;

        // Tracks whether this lizard is still being actively monitored
        public bool IsActive { get; private set; }

        // Tracks events, notes, and general temperature readings for this lizard
        public Record[] MainEvents = new Record[Constants.NUM_LIZARD_EVENTS]; 
        public List<Record> Notes = new List<Record>();
        public List<Record> TempRecords = new List<Record>();
        // Allows for a single lock on all these record objects (handles some multi-threading issues)
        public readonly object AllNotesLock = new object();

        protected readonly TimeSpan MinimumTempRecordInterval = TimeSpan.FromMilliseconds(250);

        /// <summary>
        /// Returns a complete, sorted list of events and notes on this lizard
        /// </summary>
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

        /// <summary>
        /// Creates a new lizard with the given ID number
        /// </summary>
        /// <param name="LizardNumber">The ID number for this lizard</param>
        internal LizardData(int LizardNumber)
        {
            Number = LizardNumber;
            IsActive = true;
        }

        /// <summary>
        /// Updates the lizard's current temperature, recording and publicizing the fact
        /// </summary>
        /// <param name="Temperature">The lizard's new temperature</param>
        internal void Update(double Temperature)
        {
            if (IsActive)
            {
                CurrentLizardTemp = Temperature;
                //if (TempRecords.Count == 0 || TempRecords.Last().Timestamp - DateTime.Now > MinimumTempRecordInterval)
                //{
                    TempRecords.Add(new Record(this));
                    if (OnNewData != null)
                        OnNewData(this, Temperature);
                //}
            }
        }

        /// <summary>
        /// Stops this lizard from accepting any new data
        /// </summary>
        public void Stop()
        {
            IsActive = false;
        }
    }
}
