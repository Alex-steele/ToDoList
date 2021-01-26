using System.Linq;
using ToDoList.Data.Entities;

namespace ToDoList.Data.QueryableProviders
{
    public interface IListItemQueryableProvider
    {
        IQueryable<ListItem> ListItems { get; set; }
    }
}