using CommandLine;
using Microsoft.Extensions.Logging;
using ToDoList.Console.Arguments;
using ToDoList.Console.Runners.Interfaces;

namespace ToDoList.Console.Runners
{
    public class ToDoListRunner : IToDoListRunner
    {
        private readonly ILogger logger;
        private readonly IAddCommandRunner addCommandRunner;
        private readonly ICompleteCommandRunner completeCommandRunner;
        private readonly IGetListQueryRunner getListQueryRunner;

        public ToDoListRunner(ILogger logger,
            IAddCommandRunner addCommandRunner,
            ICompleteCommandRunner completeCommandRunner,
            IGetListQueryRunner getListQueryRunner)
        {
            this.logger = logger;
            this.addCommandRunner = addCommandRunner;
            this.completeCommandRunner = completeCommandRunner;
            this.getListQueryRunner = getListQueryRunner;
        }

        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<AddCommandArguments, CompleteCommandArguments, GetListQueryArguments>(args)
                .WithParsed<AddCommandArguments>(arguments =>
                {
                    logger.LogInformation(
                        $"Running add command with arguments: {nameof(arguments.ItemValue)} = {arguments.ItemValue}");

                    addCommandRunner.Run(arguments);
                })
                .WithParsed<CompleteCommandArguments>(arguments =>
                {
                    logger.LogInformation(
                        $"Running complete command with arguments: {nameof(arguments.ItemId)} = {arguments.ItemId}");

                    completeCommandRunner.Run(arguments);
                })
                .WithParsed<GetListQueryArguments>(arguments =>
                {
                    logger.LogInformation("Running display list query");

                    getListQueryRunner.Run();
                })
                .WithNotParsed(error => logger.LogError("An error occurred while mapping to a command", error));
        }
    }
}
