using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core;
using ToDoList.Core.Commands;
using ToDoList.Core.Queries;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories;

namespace ToDoList.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new ToDoListRepository();

            var addCommand = new AddCommand(repository);
            var completeCommand = new CompleteCommand(repository);
            var getListQuery = new GetListQuery(repository);

            var toDoListRunner = new ToDoListRunner(addCommand, completeCommand, getListQuery);

            System.Console.WriteLine("Welcome to your To-Do list");

            while (true)
            {
                System.Console.WriteLine("\nType to add an item, type an item's index to mark as completed, type d to display the list or type q to quit");

                var input = System.Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                var result = toDoListRunner.Execute(input);

                switch (result.Result)
                {
                    case RunnerResult.Success:
                        DisplayList(result.Payload);
                        break;

                    case RunnerResult.ValidationError:
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Please type at least one character");
                        System.Console.ResetColor();
                        break;

                    case RunnerResult.InvalidOperation:
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Something went wrong");
                        System.Console.ResetColor();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void DisplayList(IReadOnlyCollection<ListItem> listItems)
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
                }
                System.Console.WriteLine($"{item.Id}: {item.Value}");
                System.Console.ResetColor();
            }
        }
    }
}
