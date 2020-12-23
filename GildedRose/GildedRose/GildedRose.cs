using System;
using System.Collections.Generic;

// Mikaela Stanislav / Technical Showcase for Lean Techniques
// December 2020.  mstanislav05@gmail.com

namespace GildedRose
{
    class GildedRose
    {
        static void Main(string[] args)
        {
            List<Item> itemList = new List<Item>();
            List<Item> itemChoices = new List<Item>();
            itemChoices.Add(new Item("Aged Brie", 0, 0, false));
            itemChoices.Add(new Item("Backstage passes to a TAFKAL80ETC concert", 0, 0, false));
            itemChoices.Add(new Item("Sulfuras, Hand of Ragnaros", 0, 0, false));
            itemChoices.Add(new Item("Winter Cap", 0, 0, false));
            itemChoices.Add(new Item("Greatsword", 0, 0, false));

            Console.WriteLine("Please enter how many days to run");
            string dayInput = Console.ReadLine();
            int runDaysNum;
            bool result = int.TryParse(dayInput, out runDaysNum);
            while (result != true)
            {
                Console.WriteLine("Please enter how many days to run (must be numeric)");
                dayInput = Console.ReadLine();
                result = int.TryParse(dayInput, out runDaysNum);
            }

            for (int day = 1; day <= runDaysNum; day++)
            {
                if (day == 1)
                {
                    //Ask user to stock starting inventory
                    Console.WriteLine("Please enter item number to add to starting inventory. Enter -1 to finish.");
                    foreach (var itemChoice in itemChoices)
                    {
                        Console.WriteLine(("[" + (itemChoices.IndexOf(itemChoice) + 1) + "]" + itemChoice.name));
                    }
                    bool validChoice = int.TryParse(Console.ReadLine(), out int inputNum);
                    while (validChoice != true)
                    {
                        Console.WriteLine("Please enter item number to add to starting inventory. Entry must be numeric. Enter -1 to finish.");
                        foreach (var itemChoice in itemChoices)
                        {
                            Console.WriteLine(("[" + (itemChoices.IndexOf(itemChoice) + 1) + "]" + itemChoice.name));
                        }
                        validChoice = int.TryParse(Console.ReadLine(), out inputNum);
                    }
                    while (inputNum != -1)
                    {
                        if (inputNum > 0 && inputNum <= itemChoices.Count)
                        {
                            Item chosenItem = itemChoices[inputNum - 1];
                            Item newItem;
                            if (chosenItem.name != "Sulfuras, Hand of Ragnaros")
                            {
                                int sellinDays;
                                int qualityStart;
                                int conjuredNum;
                                bool conjuredStatus = false;

                                Console.WriteLine("Please enter sell in days for new item '" + chosenItem.name + "'");
                                while (int.TryParse(Console.ReadLine(), out sellinDays) == false || sellinDays < 0)
                                {
                                    Console.WriteLine("Please enter sell in days for new item '" + chosenItem.name + "' (minimum of 0)");
                                }

                                Console.WriteLine("Please enter starting quality for new item '" + chosenItem.name + "'");
                                while (int.TryParse(Console.ReadLine(), out qualityStart) == false || qualityStart > 50 || qualityStart < 0)
                                {
                                    Console.WriteLine("Please enter starting quality for new item '" + chosenItem.name + "' (maximum of 50)");
                                }

                                Console.WriteLine("Please enter Conjured status (0 = false, 1 = true)");
                                while (int.TryParse(Console.ReadLine(), out conjuredNum) == false || (conjuredNum != 0 && conjuredNum != 1))
                                {
                                    Console.WriteLine("Please enter Conjured status (0 = false, 1 = true)");
                                }
                                if (conjuredNum == 1)
                                {
                                    conjuredStatus = true;
                                }

                                newItem = new Item(chosenItem.name, sellinDays, qualityStart, conjuredStatus);
                            }
                            else
                            {
                                //Do not request custom settings- defaults used for Sulfuras, Hand of Ragnaros
                                newItem = new Item(chosenItem.name, 0, 80, false);
                                Console.WriteLine("Sulfuras, Hand of Ragnaros successsfully added to inventory.");
                            }
                            itemList.Add(newItem);
                        }
                        Console.WriteLine("Please enter item number to add to starting inventory. Enter -1 to finish.");
                        foreach (var itemChoice in itemChoices)
                        {
                            Console.WriteLine(("[" + (itemChoices.IndexOf(itemChoice) + 1) + "]" + itemChoice.name));
                        }
                        _ = int.TryParse(Console.ReadLine(), out inputNum);
                    }
                    Console.WriteLine("Welcome to the Gilded Rose! \nHere is a list of our current inventory:");
                }

                //Display Inventory and stats for each day
                Console.WriteLine("Day " + day);
                foreach (var item in itemList)
                {
                    Console.WriteLine(item.name);
                    Console.WriteLine("\tSell Days Remaining: " + item.sell_in);
                    Console.WriteLine("\tQuality: " + item.quality);
                    Console.WriteLine("\tConjured? " + item.conjuredInd);
                }
                Console.WriteLine("Press any key to advance a day");
                Console.ReadLine();

                foreach (Item item in itemList)
                {
                    item.update_quality(item);
                }
            }
        }
    }
}