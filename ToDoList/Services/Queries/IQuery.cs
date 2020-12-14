using System.Reactive;

namespace ToDoList.Core.Services.Queries
{
    public interface IQuery<in TIn, TOut>
    {
        QueryResultWrapper<TOut> Execute(TIn model);
    }

    public interface IQuery<TOut> : IQuery<Unit, TOut>
    {
    }
}
