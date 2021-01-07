using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IGetListQuery
    {
        QueryResultWrapper Execute();
    }
}