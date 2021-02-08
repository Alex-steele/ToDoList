using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Data.Entities;

namespace ToDoList.Core.Mappers
{
    public class ListItemMapper : IListItemMapper
    {
        public ListItemModel Map(ListItem listItem)
        {
            return new ListItemModel
            {
                Id = listItem.Id,
                Value = listItem.Value,
                Completed = listItem.Completed,
                Date = listItem.Date
            };
        }
    }
}
