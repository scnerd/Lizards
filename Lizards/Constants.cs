using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Lizards
{
    public static class Constants
    {

        // The SerialPort connection settings, must be synchronized with how the Arduino sets up its serial communication
        public const Parity PARITY = Parity.Even;
        public const StopBits STOP_BITS = StopBits.One;
        public const int BITS_PER_DATA = 8;
        public const int BAUD_RATE = 9600;

        // Default value for how many seconds should elapse before saving another temperature reading for each lizard
        public const int DEFAULT_SAVE_INTERVAL = 15;

        // Defines the thermometer settings for the manifold (ambient) and lizard thermometers
        //      Scale: 1 / How many integer values elapse between a single degree celsius (e.g., a 1/16 deg C resolution thermometer would use 1/16 as the scale)
        //      Base: The temperature in deg C when the thermometer reports the 0 value
        public const double AMBIENT_SCALE = 1 / 16d, AMBIENT_BASE = 0d;
        public const double LIZARD_SCALE = 1 / 16d, LIZARD_BASE = 0d;

        // Names for each major event that all lizards will go through (changing the number of this will require
        // changing the UI's manually). Changing these strings will automatically propogate through the UI's.
        public static string[] LIZARD_EVENTS = { "Panting", "LRR", "Spasming (stop)" };
        public static int NUM_LIZARD_EVENTS = LIZARD_EVENTS.Length;



    }
}
