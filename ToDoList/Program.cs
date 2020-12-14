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

                if (InputIsItemIndex(listItems, input))
                {
                    CompleteItem(listItems, input);

                    DisplayList(listItems);

                    continue;
                }

                if (input == "d")
                {
                    DisplayList(listItems);

                    continue;
                }

                AddItem(listItems, input);
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

        private static void CompleteItem(List<ListItem> listItems, string input)
        {
            var completedItem = listItems.Single(x => x.Id == int.Parse(input));
            completedItem.Complete();
        }

        private static void AddItem(List<ListItem> listItems, string input)
        {
            listItems.Add(new ListItem
            {
                Id = listItems.Max(x => x?.Id) + 1 ?? 1,
                Value = input,
                Completed = false
            });
        }

        private static bool InputIsItemIndex(List<ListItem> listItems, string input)
        {
            return int.TryParse(input, out var index) && listItems.Any(x => x.Id == index);
        }
    }
}
