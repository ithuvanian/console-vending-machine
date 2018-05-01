using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVendingMachine
{
    public class Item
    {
        public string SlotID { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Qty { get; set; }
        public int Sold { get; set; }

        public Item(string slotID, string name, decimal price)
        {
            SlotID = slotID;
            Name = name;
            Price = price;
            Qty = 5;
            Sold = 0;
        }
    }
}
