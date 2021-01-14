using System.Threading.Tasks;
using ToDoList.Console.Arguments;

namespace ToDoList.Console.Runners.Interfaces
{
    public interface ICompleteCommandRunner
    {
        Task RunAsync(CompleteCommandArguments arguments);
    }
}