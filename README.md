This is a C# .NET console application that can be run from MS Visual Studio.

# console-vending-machine
Tech Elevator pair project, week 4

This project was built to simulate a vending machine from the program console, with the following requirements:

---
1. A main menu gives the user an option to view all items or make a purchase.
    - A csv file is provided with all the items in the machine, and a corresponding item code and price
    - All items are initialized with 5 in stock and zero sold

---
2. If the user elects to make a purchase, the next line gives options to 
    - insert money
    - select a product
    - finish the transaction
    - cancel the transaction
  
---  
3. The user can "insert money" in the form of a whole integer, which is translated into dollars.
    - The program keeps track of the amount of money inserted
 
---
4. The user can select a product by entering the corresponding product code.
    - The balance of money inserted is updated when an item is selected
  
---  
5. The user can finish a transaction
    - The machine returns change to the customer in quarters, nickels and dimes according to the remaining balance.
        -- Change should be in the smallest number of coins possible
    - The program displays each item being dispensed and prints out a message according to the type of item
  
---  
6. The user can cancel a transaction
    - The machine returns the customer's entire balance of money
  
---  
7. The program keeps a record of all transactions (feed money, give change, dispense item) and writes them to a text file (log.txt)

---
8. The program keeps a running balance of:
    - revenue from items purchased
    - each item's qty in stock
        -- If an item is sold out, the user is not able to purchase it

---
9. The user can generate a new file with a sales report for all items - *in our program, this is done by pressing 3 from the main menu*
