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
            Dictionary<string, int> initialCoins = new Dictionary<string, int>
            {
                {"quarters", 0 },
                {"dimes", 0 },
                {"nickels", 0 }
            };
            VendingMachine vendingMachine = new VendingMachine(0, 0, initialCoins);
            UserInterface userInterface = new UserInterface();
            while (true)
            {
                userInterface.RunInterface(vendingMachine);
            }
        }
    }
}
