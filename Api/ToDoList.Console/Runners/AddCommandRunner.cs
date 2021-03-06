using System.Threading.Tasks;
using ToDoList.Console.Arguments;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Core.Commands.Interfaces;

namespace ToDoList.Console.Runners
{
    public class AddCommandRunner : IAddCommandRunner
    {
        private readonly IAddCommand addCommand;
        private readonly IAddCommandArgumentMapper argumentMapper;
        private readonly IAddResultHandler resultHandler;

        public AddCommandRunner(IAddCommand addCommand,
            IAddCommandArgumentMapper argumentMapper,
            IAddResultHandler resultHandler)
        {
            this.addCommand = addCommand;
            this.argumentMapper = argumentMapper;
            this.resultHandler = resultHandler;
        }
        public async Task RunAsync(AddCommandArguments arguments)
        { 
            var result = await addCommand.ExecuteAsync(argumentMapper.Map(arguments));

            resultHandler.Handle(result);
        }
    }
}
