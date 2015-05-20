using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lizards;

namespace LizardsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string possible_port in ArduinoCommunicator.GetPossiblePorts())
                Console.WriteLine(possible_port);
            Console.WriteLine("Press any key to quit...");
            Console.ReadLine(); // Wait before closing
        }
    }
}
