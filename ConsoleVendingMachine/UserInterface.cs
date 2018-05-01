using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVendingMachine
{
    public class UserInterface
    {
        public List<Item> purchasedItems = new List<Item>();

        public void RunInterface(VendingMachine vendingMachine)
        {
            bool finished = false;
            Console.WriteLine("Press 1 to view items, or 2 to make a purchase:");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    UserDisplayItems(vendingMachine);
                    break;
                case "2":
                    while (finished == false)
                    {
                        finished = PurchaseMenu(vendingMachine);
                    }
                    break;
                case "3":
                    vendingMachine.CreateReport();
                    Console.WriteLine("**Sales report created in ConsoleVendingMachine/bin/debug/ folder**");
                    break;
                default:
                    Console.WriteLine("Input not recognized");
                    break;
            }
        }

        public void UserDisplayItems(VendingMachine vendingMachine)
        {
            foreach (KeyValuePair<string, Item> listing in vendingMachine.itemsInMachine)
            {
                Item thisItem = listing.Value;
                string itemStatus = "";
                if (thisItem.Qty == 0)
                {
                    itemStatus = "SOLD OUT";
                }
                Console.WriteLine("{0,5}{1,25}{2,10}{3,10}", thisItem.SlotID, thisItem.Name, "$" + thisItem.Price, itemStatus);
            }
            Console.WriteLine();
        }

        public bool PurchaseMenu(VendingMachine vendingMachine)
        {
            bool selectionMade = false;
            Console.WriteLine("Press 1 to insert money, 2 to select a product, 3 to finish transaction, or 0 to cancel transaction:");
            Console.WriteLine($"Current money provided: {vendingMachine.CurrentMoneyProvided.ToString("$0.00")}");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    foreach (Item purchasedItem in purchasedItems)
                    {
                        vendingMachine.CancelPurchase(purchasedItem);
                    }
                    purchasedItems.Clear();
                    UserFinishTransaction(vendingMachine);
                    return true;
                case "1":
                    UserFeedMoney(vendingMachine);
                    return false;
                case "2":
                    if (vendingMachine.CurrentMoneyProvided > 0)
                    {
                        while (selectionMade == false)
                        {
                            selectionMade = UserSelectProduct(vendingMachine, selectionMade);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please insert money before selecting a product");
                        Console.WriteLine();
                    }
                    return false;
                case "3":
                    UserFinishTransaction(vendingMachine);
                    return true;
                default:
                    Console.WriteLine("Input not recognized");
                    return false;
            }
        }

        public void UserFeedMoney(VendingMachine vendingMachine)
        {
            Console.WriteLine("Please insert dollars into the machine (represented by an integer):");
            string userInput = Console.ReadLine();
            int.TryParse(userInput, out int dollarAmt);
            Console.WriteLine($"Inserted {dollarAmt.ToString("$0.00")}");
            vendingMachine.FeedMoney(dollarAmt);
        }

        public bool UserSelectProduct(VendingMachine vendingMachine, bool selectionMade)
        {
            Console.WriteLine("Please enter a product code. Press 1 to show all items or 0 to go back:");
            string userInput = Console.ReadLine().ToUpper();

            if (userInput == "0")
            {
                return true;
            }
            if (userInput == "1")
            {
                UserDisplayItems(vendingMachine);
                return false;
            }
            else if (!vendingMachine.itemsInMachine.ContainsKey(userInput))
            {
                Console.WriteLine("Input does not match any item");
                Console.WriteLine();
                return false;
            }
            else
            {
                Item selectedItem = vendingMachine.itemsInMachine[userInput];
                if (selectedItem.Qty == 0)
                {
                    Console.WriteLine("Sorry, that item is sold out");
                    Console.WriteLine();
                }
                else if (vendingMachine.CurrentMoneyProvided < selectedItem.Price)
                {
                    Console.WriteLine("More money needed for your selected item");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Selected {selectedItem.Name}");
                    purchasedItems.Add(selectedItem);
                    vendingMachine.MakePurchase(selectedItem);
                }
                return true;
            }
        }

        public void UserFinishTransaction(VendingMachine vendingMachine)
        {
            vendingMachine.GiveChange();
            foreach (KeyValuePair<string, int> coinType in vendingMachine.coins)
            {
                if (coinType.Value > 0)
                {
                    Console.WriteLine($"Returning {coinType.Value} {coinType.Key}");
                }
            }
            vendingMachine.Reset();
            Console.WriteLine("...");

            foreach (Item purchasedItem in purchasedItems)
            {
                Console.WriteLine($"Dispensing {purchasedItem.Name}");
                if (purchasedItem.SlotID.StartsWith("A"))
                {
                    Console.WriteLine("Crunch Crunch, Yum!");
                }
                else if (purchasedItem.SlotID.StartsWith("B"))
                {
                    Console.WriteLine("Munch, Munch, Yum!");
                }
                else if (purchasedItem.SlotID.StartsWith("C"))
                {
                    Console.WriteLine("Glug, Glug, Yum!");
                }
                else if (purchasedItem.SlotID.StartsWith("D"))
                {
                    Console.WriteLine("Chew, Chew, Yum!");
                }
                Console.WriteLine("...");
            }
            Console.WriteLine();
        }
    }
}
