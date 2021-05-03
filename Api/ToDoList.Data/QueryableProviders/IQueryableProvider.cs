using System.Linq;

namespace ToDoList.Data.QueryableProviders
{
    public interface IQueryableProvider<T> where T : class
    {
        IQueryable<T> Set { get;}
    }
}