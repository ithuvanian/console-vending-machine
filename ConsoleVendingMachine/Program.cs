using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            UserInterface userInterface = new UserInterface();
            while (true)
            {
                userInterface.RunInterface(vendingMachine);
            }
        }
    }
}
