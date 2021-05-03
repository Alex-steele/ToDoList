using System.Collections.Generic;
using ToDoList.Core.Models;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IGetItemByValueQuery : IQuery<GetItemByValueQueryModel, List<ListItemModel>>
    {
    }
}