using ToDoList.Console.Arguments;

namespace ToDoList.Console.Runners.Interfaces
{
    public interface ICompleteCommandRunner
    {
        void Run(CompleteCommandArguments arguments);
    }
}