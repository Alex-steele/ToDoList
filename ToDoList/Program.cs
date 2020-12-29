using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core;
using ToDoList.Core.Commands;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Mappers;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Console
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();

            var toDoListRunner = serviceProvider.GetService<IToDoListRunner>();

            System.Console.WriteLine("Welcome to your To-Do list");

            while (true)
            {
                System.Console.WriteLine("\nType to add an item, type an item's index to mark as completed or type q to quit");

                var input = System.Console.ReadLine();

                if (input == "q")
                {
                    DisposeServices();
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

            DisposeServices();
        }

        private static void DisplayList(IReadOnlyCollection<ListItemModel> listItems)
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

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            var builder = new ContainerBuilder();

            builder.RegisterType<ToDoListRunner>().As<IToDoListRunner>();
            builder.RegisterType<AddCommand>().As<IAddCommand>();
            builder.RegisterType<CompleteCommand>().As<ICompleteCommand>();
            builder.RegisterType<GetListQuery>().As<IGetListQuery>();
            builder.RegisterType<UserInputValidator>().As<IUserInputValidator>();
            builder.RegisterType<ListItemMapper>().As<IListItemMapper>();
            builder.RegisterType<ToDoListRepository>().As<IToDoListRepository>().InstancePerLifetimeScope();

            builder.Populate(collection);

            var appContainer = builder.Build();

            serviceProvider = new AutofacServiceProvider(appContainer);
        }

        private static void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }

            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
