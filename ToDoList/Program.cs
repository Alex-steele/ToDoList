using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your To-Do list");

            var listItems = new List<ListItem>();

            while (true)
            {
                Console.WriteLine("\nType to add an item, type and item's index to mark as completed, type d to display the list or type q to quit");

                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input == "q")
                {
                    break;
                }

                if (int.TryParse(input, out var index) && listItems.Any(x => x.Id == index))
                {
                    var completedItem = listItems.Single(x => x.Id == index);
                    completedItem.Completed = true;
                    completedItem.Value += " --Completed";


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nItem successfully completed");
                    Console.ResetColor();

                    continue;
                }

                if (input == "d")
                {
                    if (!listItems.Any())
                    {
                        Console.WriteLine("\nYour To-Do list is empty");
                        continue;
                    }

                    Console.WriteLine("\nTo-Do List");
                    Console.WriteLine("----------");

                    foreach (var item in listItems)
                    {
                        if (item.Completed)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.WriteLine($"{item.Id}: {item.Value}");
                        Console.ResetColor();
                    }

                    continue;
                }

                listItems.Add(new ListItem
                {
                    Id = listItems.Max(x => x?.Id) + 1 ?? 1,
                    Value = input,
                    Completed = false
                });
            }
        }
    }
}
