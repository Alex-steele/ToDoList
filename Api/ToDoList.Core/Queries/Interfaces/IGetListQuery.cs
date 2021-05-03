using System.Collections.Generic;
using ToDoList.Core.Models;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IGetListQuery : IQuery<List<ListItemModel>>
    {
    }
}