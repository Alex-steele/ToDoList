using System;
using CommandLine;
using Microsoft.Extensions.Logging;
using ToDoList.Console.Arguments;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Queries.Interfaces;
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
                IToDoListServiceContainer serviceProvider = new ToDoListServiceContainer();

                var logger = serviceProvider.GetService<ILogger<Program>>();

                Parser.Default.ParseArguments<AddCommandArguments, CompleteCommandArguments, GetListQueryArguments>(args)
                    .WithParsed<AddCommandArguments>(arguments =>
                    {
                        logger.LogInformation(
                            $"Running add command with arguments: {nameof(arguments.ItemValue)} = {arguments.ItemValue}");

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
                        logger.LogInformation(
                            $"Running complete command with arguments: {nameof(arguments.ItemId)} = {arguments.ItemId}");

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
                    })
                    .WithParsed<GetListQueryArguments>( arguments =>
                    {
                        logger.LogInformation("Running display list query");

                        var getListQuery = serviceProvider.GetService<IGetListQuery>();

                        var result = getListQuery.Execute();

                        switch (result.Result)
                        {
                            case QueryResult.Success:
                                DisplayList.Display(result.Payload);
                                break;

                            case QueryResult.Error:
                                WriteMessage.Error($"Could not retrieve list");
                                break;
                        }
                    })
                    .WithNotParsed(error => logger.LogError("An error occurred while mapping to a command", error));

            }
            catch (Exception ex)
            {
                WriteMessage.Error($"An unhandled error occurred: {ex}");
            }

            System.Console.WriteLine("Press any key to close");

            System.Console.ReadKey();
        }
    }
}
