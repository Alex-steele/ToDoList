using ToDoList.Console.Arguments;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Core.Commands.Interfaces;

namespace ToDoList.Console.Runners
{
    public class CompleteCommandRunner : ICompleteCommandRunner
    {
        private readonly ICompleteCommand completeCommand;
        private readonly ICompleteCommandArgumentMapper argumentMapper;
        private readonly ICompleteResultHandler resultHandler;

        public CompleteCommandRunner(ICompleteCommand completeCommand,
            ICompleteCommandArgumentMapper argumentMapper,
            ICompleteResultHandler resultHandler)
        {
            this.completeCommand = completeCommand;
            this.argumentMapper = argumentMapper;
            this.resultHandler = resultHandler;
        }
        public void Run(CompleteCommandArguments arguments)
        {
            var result = completeCommand.Execute(argumentMapper.Map(arguments));

            resultHandler.Handle(result, arguments);
        }
    }
}
