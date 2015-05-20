using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lizards
{
    public delegate void NewLizardDataHandler(object sender, LizardData newData);

    public class LizardData
    {
        public event NewLizardDataHandler OnNewData;
    }
}
