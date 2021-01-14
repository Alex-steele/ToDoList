using System.Threading.Tasks;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IQuery
    {
        Task<QueryResultWrapper> ExecuteAsync();
    }
}
