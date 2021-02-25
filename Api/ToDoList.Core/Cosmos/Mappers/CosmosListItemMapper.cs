using ToDoList.Core.Cosmos.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Data.Cosmos.Entities;

namespace ToDoList.Core.Cosmos.Mappers
{
    public class CosmosListItemMapper : ICosmosListItemMapper
    {
        public ListItemModel Map(CosmosListItem listItem)
        {
            return new ListItemModel
            {
                Id = listItem.IntId,
                Value = listItem.Value,
                Completed = listItem.Completed
            };
        }
    }
}