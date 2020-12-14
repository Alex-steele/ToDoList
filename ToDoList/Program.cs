using System;
using System.Collections.Generic;

namespace ToDoList.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type a message to add to your To-Do List");

            var listItems = new List<string>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                if (input == "d")
                {
                    Console.WriteLine("\nTo-Do List");
                    Console.WriteLine("----------");
                    foreach (var item in listItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    listItems.Add(input);
                }

                Console.WriteLine("\nAdd another item or type d to display the list or type q to quit");
            }
        }
    }
}
