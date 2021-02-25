using ToDoList.Console.Arguments;
using ToDoList.Core.Wrappers;

namespace ToDoList.Console.ResultHandlers.Interfaces
{
    public interface ICompleteResultHandler
    {
        void Handle(CommandResultWrapper result, CompleteCommandArguments arguments);
    }
}