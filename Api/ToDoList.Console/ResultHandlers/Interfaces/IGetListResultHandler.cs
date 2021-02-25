using ToDoList.Core.Wrappers;

namespace ToDoList.Console.ResultHandlers.Interfaces
{
    public interface IGetListResultHandler
    {
        void Handle(QueryResultWrapper result);
    }
}