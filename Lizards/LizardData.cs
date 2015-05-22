using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lizards
{
    public delegate void NewLizardDataHandler(LizardData newData, double newTemp);

    public class LizardData
    {
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

        public LizardData(int LizardNumber)
        {
            Number = LizardNumber;
        }

        internal void Update(double Temperature)
        {
            Temperatures.Add(Temperature);
            if(OnNewData != null)
                OnNewData(this, Temperature);
        }
    }
}
