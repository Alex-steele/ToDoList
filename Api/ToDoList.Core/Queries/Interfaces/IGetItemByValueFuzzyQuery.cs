using System.Collections.Generic;
using ToDoList.Core.Models;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IGetItemByValueFuzzyQuery : IQuery<GetItemByValueQueryModel, List<ListItemModel>>
    {
    }
}