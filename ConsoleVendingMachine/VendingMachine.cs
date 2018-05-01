using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleVendingMachine
{
    public class VendingMachine
    {
        public decimal MoneyInMachine { get; set; }
        public decimal CurrentMoneyProvided { get; set; }

        public Dictionary<string, Item> itemsInMachine = new Dictionary<string, Item>();
        public Dictionary<string, int> coins = new Dictionary<string, int>
            {
                {"quarters", 0 },
                {"dimes", 0 },
                {"nickles", 0 }
            };


        public VendingMachine()
        {
            BuildInventory();
            CurrentMoneyProvided = 0;
            MoneyInMachine = 0;
        }

        public void BuildInventory()
        {
            string dir = Environment.CurrentDirectory;
            string fileName = "vendingmachine.csv";
            string fullPath = Path.Combine(dir, fileName);
            using (StreamReader sr = new StreamReader(fullPath))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|');
                    string slotID = line[0];
                    string itemName = line[1];
                    decimal itemPrice = decimal.Parse(line[2]);
                    Item thisItem = new Item(slotID, itemName, itemPrice);
                    itemsInMachine.Add(slotID, thisItem);
                }

            }
        }

        public void FeedMoney(int dollars)
        {
            CurrentMoneyProvided += dollars;
            string dir = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string fullPath = Path.Combine(dir, fileName);
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.Write("{0,-50}{1,-10}{2,-10}\n", $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} FEED MONEY", dollars.ToString("$0.00"), CurrentMoneyProvided.ToString("$0.00"));
            }
        }

        public void MakePurchase(Item selectedItem)
        {
            decimal beginningBalance = CurrentMoneyProvided;
            selectedItem.Qty--;
            selectedItem.Sold++;
            CurrentMoneyProvided -= selectedItem.Price;
            MoneyInMachine += selectedItem.Price;

            string dir = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string fullPath = Path.Combine(dir, fileName);
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.Write("{0,-50}{1,-10}{2,-10}\n", $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} {selectedItem.Name} {selectedItem.SlotID} ", beginningBalance.ToString("$0.00"), CurrentMoneyProvided.ToString("$0.00"));
            }
        }

        public void CancelPurchase(Item selectedItem)
        {
            decimal beginningBalance = CurrentMoneyProvided;
            selectedItem.Qty++;
            selectedItem.Sold--;
            CurrentMoneyProvided += selectedItem.Price;
            MoneyInMachine -= selectedItem.Price;

            string dir = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string fullPath = Path.Combine(dir, fileName);
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.Write("{0,-50}{1,-10}{2,-10}\n", $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} CANCEL {selectedItem.Name} {selectedItem.SlotID} ", beginningBalance.ToString("$0.00"), CurrentMoneyProvided.ToString("$0.00"));
            }
        }

        public void GiveChange()
        {
            decimal beginningBalance = CurrentMoneyProvided;
            while (CurrentMoneyProvided >= .25M)
            {
                CurrentMoneyProvided -= .25M;
                coins["quarters"]++;
            }
            while (CurrentMoneyProvided >= .1M)
            {
                CurrentMoneyProvided -= .1M;
                coins["dimes"]++;
            }
            while (CurrentMoneyProvided > 0)
            {
                CurrentMoneyProvided -= .05M;
                coins["nickles"]++;
            }
            string dir = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string fullPath = Path.Combine(dir, fileName);
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.Write("{0,-50}{1,-10}{2,-10}\n", $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} GIVE CHANGE", beginningBalance.ToString("$0.00"), CurrentMoneyProvided.ToString("$0.00"));
            }
        }

        public void Reset()
        {
            coins["quarters"] = 0;
            coins["dimes"] = 0;
            coins["nickles"] = 0;
        }

        public void CreateReport()
        {
            string dir = Environment.CurrentDirectory;
            string fileName = "salesReport.txt";
            string fullPath = Path.Combine(dir, fileName);
            using (StreamWriter sw = new StreamWriter(fullPath, false))

            {
                foreach (KeyValuePair<string, Item> item in itemsInMachine)
                {
                    sw.WriteLine($"{item.Value.Name} | {item.Value.Sold}");
                }
                sw.WriteLine();
                sw.WriteLine($"**TOTAL SALES** {MoneyInMachine.ToString("$0.00")}");
            }
        }
    }
}
