using System.Linq;

namespace ToDoList.Data.QueryableProviders
{
    public class QueryableProvider<T> : IQueryableProvider<T> where T : class
    {
        public QueryableProvider(ToDoListContext context)
        {
            Set = context.Set<T>();
        }

        public IQueryable<T> Set { get;}
    }
}
