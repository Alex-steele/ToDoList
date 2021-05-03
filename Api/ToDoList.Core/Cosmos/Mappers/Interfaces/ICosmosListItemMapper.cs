using ToDoList.Core.Models;
using ToDoList.Data.Cosmos.Entities;

namespace ToDoList.Core.Cosmos.Mappers.Interfaces
{
    public interface ICosmosListItemMapper
    {
        ListItemModel Map(CosmosListItem listItem);
    }
}
