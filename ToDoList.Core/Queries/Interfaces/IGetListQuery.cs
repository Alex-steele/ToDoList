using System.Collections.Generic;
using ToDoList.Data.Entities;

namespace ToDoList.Core.Queries.Interfaces
{
    public interface IGetListQuery
    {
        List<ListItem> GetList();
    }
}