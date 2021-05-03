using System.Threading.Tasks;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IQuery<in TIn, TOut>
    {
        Task<QueryResultWrapper<TOut>> ExecuteAsync(TIn model);
    }

    public interface IQuery<TOut>
    {
        Task<QueryResultWrapper<TOut>> ExecuteAsync();
    }
}
