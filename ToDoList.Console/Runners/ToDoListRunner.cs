using CommandLine;
using Microsoft.Extensions.Logging;
using ToDoList.Console.Arguments;
using ToDoList.Console.Runners.Interfaces;

namespace ToDoList.Console.Runners
{
    public class ToDoListRunner : IToDoListRunner
    {
        private readonly ILogger<ToDoListRunner> logger;
        private readonly IAddCommandRunner addCommandRunner;
        private readonly ICompleteCommandRunner completeCommandRunner;
        private readonly IGetListQueryRunner getListQueryRunner;

        public ToDoListRunner(ILogger<ToDoListRunner> logger,
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
                .WithParsed<AddCommandArguments>(async arguments =>
                {
                    logger.LogInformation(
                        $"Running add command with arguments: {nameof(arguments.ItemValue)} = {arguments.ItemValue}");

                    await addCommandRunner.RunAsync(arguments);
                })
                .WithParsed<CompleteCommandArguments>(async arguments =>
                {
                    logger.LogInformation(
                        $"Running complete command with arguments: {nameof(arguments.ItemId)} = {arguments.ItemId}");

                    await completeCommandRunner.RunAsync(arguments);
                })
                .WithParsed<GetListQueryArguments>(async arguments =>
                {
                    logger.LogInformation("Running display list query");

                    await getListQueryRunner.RunAsync();
                })
                .WithNotParsed(error => logger.LogError("An error occurred while mapping to a command", error));
        }
    }
}
