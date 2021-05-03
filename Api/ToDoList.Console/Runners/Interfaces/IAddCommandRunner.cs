using System.Threading.Tasks;
using ToDoList.Console.Arguments;

namespace ToDoList.Console.Runners.Interfaces
{
    public interface IAddCommandRunner
    {
        Task RunAsync(AddCommandArguments args);
    }
}