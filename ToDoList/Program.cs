using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Commands;

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
                Console.WriteLine("\nType to add an item, type an item's index to mark as completed, type d to display the list or type q to quit");

                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input == "q")
                {
                    break;
                }

                if (input == "d")
                {
                    DisplayList(listItems);

                    continue;
                }

                if (InputIsItemId(listItems, input))
                {
                    CompleteCommand.CompleteItem(listItems, int.Parse(input));

                    DisplayList(listItems);

                    continue;
                }

                AddCommand.AddItem(listItems, input);
            }
        }

        private static void DisplayList(List<ListItem> listItems)
        {
            if (!listItems.Any())
            {
                Console.WriteLine("\nYour To-Do list is empty");
                return;
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
        }

        private static bool InputIsItemId(List<ListItem> listItems, string input)
        {
            return int.TryParse(input, out var id) && listItems.Any(x => x.Id == id);
        }
    }
}
