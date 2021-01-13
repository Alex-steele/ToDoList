using ToDoList.Core.Wrappers;

namespace ToDoList.Console.ResultHandlers.Interfaces
{
    public interface IAddResultHandler
    {
        void Handle(CommandResultWrapper result);
    }
}