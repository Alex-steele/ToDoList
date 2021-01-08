using CommandLine;
using Microsoft.Extensions.Logging;
using ToDoList.Console.Arguments;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Console.Messages;
using ToDoList.Console.Runner.Interface;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console.Runner
{
    public class ToDoListRunner : IToDoListRunner
    {
        public void Run(IToDoListServiceContainer serviceProvider, string[] args)
        {
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
                            SuccessMessage.Write("Item successfully added");
                            break;

                        case CommandResult.ValidationError:
                            ValidationErrorMessage.Write(result.Validation);
                            break;

                        case CommandResult.Error:
                            ErrorMessage.Write("An error occurred while executing the add command");
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
                            SuccessMessage.Write("Item successfully completed");
                            break;

                        case CommandResult.NotFound:
                            ErrorMessage.Write($"Could not find item with specified Id: {arguments.ItemId}");
                            break;
                    }
                })
                .WithParsed<GetListQueryArguments>(arguments =>
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
                            ErrorMessage.Write($"Could not retrieve list");
                            break;
                    }
                })
                .WithNotParsed(error => logger.LogError("An error occurred while mapping to a command", error));
        }
    }
}
