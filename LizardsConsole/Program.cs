using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Lizards;

namespace LizardsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort port = new SerialPort("COM3", 9600, Parity.Even);
            port.StopBits = StopBits.One;
            port.DataBits = 8;
            port.DtrEnable = true;
            port.Open();
            var PrintByte = new Func<byte, string>((b) => string.Format("{0:X}", b));
                //char.IsControl((char) b) ? string.Format("{0:X}", b) : ((char) b).ToString());
            while (true)
            {
                byte res = (byte)port.ReadByte();
                Console.WriteLine(string.Format("Received: {0,4}", PrintByte(res)));
            }
            Console.WriteLine("Press any key to quit...");
            Console.ReadLine(); // Wait before closing
        }
    }
}
