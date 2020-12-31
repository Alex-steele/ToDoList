using System;
using CommandLine;
using ToDoList.Console.Arguments;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IToDoListServiceContainer serviceProvider = new ToDoListServiceContainer())
            {
                Parser.Default.ParseArguments<AddCommandArguments, CompleteCommandArguments>(args)
                    .WithParsed<AddCommandArguments>(arguments =>
                    {
                        var addCommand = serviceProvider.GetService<IAddCommand>();
                        var addCommandMapper = serviceProvider.GetService<IAddCommandArgumentMapper>();

                        var result = addCommand.Execute(addCommandMapper.Map(arguments));
                        switch (result)
                        {
                            case CommandResult.Success:
                                System.Console.ForegroundColor = ConsoleColor.Green;
                                System.Console.WriteLine("Item successfully added");
                                System.Console.ResetColor();
                                break;

                            case CommandResult.ValidationError:
                                System.Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("Invalid input");
                                System.Console.ResetColor();
                                break;

                            case CommandResult.Error:
                                System.Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("An error occurred while executing the add command");
                                System.Console.ResetColor();
                                break;
                        }
                    })
                    .WithParsed<CompleteCommandArguments>(arguments =>
                    {
                        var completeCommand = serviceProvider.GetService<ICompleteCommand>();
                        var completeCommandMapper = serviceProvider.GetService<ICompleteCommandArgumentMapper>();
                        
                        var result = completeCommand.Execute(completeCommandMapper.Map(arguments));
                        switch (result)
                        {
                            case CommandResult.Success:
                                System.Console.ForegroundColor = ConsoleColor.Green;
                                System.Console.WriteLine("Item successfully completed");
                                System.Console.ResetColor();
                                break;

                            case CommandResult.NotFound:
                                System.Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine($"Could not find item with specified Id: {arguments.ItemId}");
                                System.Console.ResetColor();
                                break;
                        }
                    });
            }
        }
    }
}
