using System.Linq;
using ToDoList.Data.Entities;

namespace ToDoList.Data.QueryableProviders
{
    public class ListItemQueryableProvider : IListItemQueryableProvider
    {
        public ListItemQueryableProvider(ToDoListContext context)
        {
            ListItems = context.ListItems;
        }
        public IQueryable<ListItem> ListItems { get; set; }
    }
}
