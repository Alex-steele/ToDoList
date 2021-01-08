using CommandLine;
using Microsoft.Extensions.Logging;
using ToDoList.Console.Arguments;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Console.ResultHandlers;
using ToDoList.Console.Runner.Interface;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Queries.Interfaces;

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

                    AddResultHandler.Handle(result);
                })
                .WithParsed<CompleteCommandArguments>(arguments =>
                {
                    logger.LogInformation(
                        $"Running complete command with arguments: {nameof(arguments.ItemId)} = {arguments.ItemId}");

                    var completeCommand = serviceProvider.GetService<ICompleteCommand>();
                    var completeCommandMapper = serviceProvider.GetService<ICompleteCommandArgumentMapper>();

                    var result = completeCommand.Execute(completeCommandMapper.Map(arguments));

                    CompleteResultHandler.Handle(result, arguments);
                })
                .WithParsed<GetListQueryArguments>(arguments =>
                {
                    logger.LogInformation("Running display list query");

                    var getListQuery = serviceProvider.GetService<IGetListQuery>();

                    var result = getListQuery.Execute();

                    GetListResultHandler.Handle(result);
                })
                .WithNotParsed(error => logger.LogError("An error occurred while mapping to a command", error));
        }
    }
}
