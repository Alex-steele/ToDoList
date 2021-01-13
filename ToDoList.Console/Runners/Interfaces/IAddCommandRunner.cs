using ToDoList.Console.Arguments;

namespace ToDoList.Console.Runners.Interfaces
{
    public interface IAddCommandRunner
    {
        void Run(AddCommandArguments args);
    }
}