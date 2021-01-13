using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Models;

namespace ToDoList.Console.Messages
{
    public class DisplayList
    {
        public static void Display(IEnumerable<ListItemModel> listItems)
        {
            if (!listItems.Any())
            {
                System.Console.WriteLine("\nYour To-Do list is empty");
                return;
            }

            System.Console.WriteLine("\nTo-Do List");
            System.Console.WriteLine("----------");

            foreach (var item in listItems)
            {
                if (item.Completed)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    item.Value += " -- Completed";
                }
                System.Console.WriteLine($"{item.Id}: {item.Value}");
                System.Console.ResetColor();
            }

        }
    }
}
