using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Lizards
{
    public class ArduinoCommunicator
    {
        public const int BAUD_RATE = 9600;
        public const Parity PARITY = Parity.Even;

        SerialPort port;

        public static string[] GetPossiblePorts()
        {
            return SerialPort.GetPortNames();
        }

        public ArduinoCommunicator(string ToConnect)
        {
            port = new SerialPort(ToConnect, BAUD_RATE, PARITY);
        }
    }
}
