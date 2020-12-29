using ToDoList.Core.Models;
using ToDoList.Data.Entities;

namespace ToDoList.Core.Mappers.Interfaces
{
    public interface IListItemMapper
    {
        ListItemModel Map(ListItem listItem);
    }
}