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
                Message.Write("\nYour To-Do list is empty");
                return;
            }

            Message.Write("\nTo-Do List");
            Message.Write("----------");

            foreach (var item in listItems)
            {
                if (item.Completed)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    item.Value += " -- Completed";
                }
                Message.Write($"{item.Id}: {item.Value}");
                System.Console.ResetColor();
            }

        }
    }
}
