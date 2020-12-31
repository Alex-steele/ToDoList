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
            System.Console.WriteLine("Starting App...");

            try
            {
                using (IToDoListServiceContainer serviceProvider = new ToDoListServiceContainer())
                {
                    Parser.Default.ParseArguments<AddCommandArguments, CompleteCommandArguments>(args)
                        .WithParsed<AddCommandArguments>(arguments =>
                        {
                            var addCommand = serviceProvider.GetService<IAddCommand>();
                            var addCommandMapper = serviceProvider.GetService<IAddCommandArgumentMapper>();

                            var result = addCommand.Execute(addCommandMapper.Map(arguments));

                            switch (result.Result)
                            {
                                case CommandResult.Success:
                                    WriteMessage.Success("Item successfully added");
                                    break;

                                case CommandResult.ValidationError:
                                    WriteMessage.ValidationError(result.Validation);
                                    break;

                                case CommandResult.Error:
                                    WriteMessage.Error("An error occurred while executing the add command");
                                    break;
                            }
                        })
                        .WithParsed<CompleteCommandArguments>(arguments =>
                        {
                            var completeCommand = serviceProvider.GetService<ICompleteCommand>();
                            var completeCommandMapper = serviceProvider.GetService<ICompleteCommandArgumentMapper>();

                            var result = completeCommand.Execute(completeCommandMapper.Map(arguments));
                            switch (result.Result)
                            {
                                case CommandResult.Success:
                                    WriteMessage.Success("Item successfully completed");
                                    break;

                                case CommandResult.NotFound:
                                    WriteMessage.Error($"Could not find item with specified Id: {arguments.ItemId}");
                                    break;
                            }
                        });
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("An unhandled error occurred: {0}", ex);
            }

            System.Console.WriteLine("Press any key to close");

            System.Console.ReadKey();
        }
    }
}
