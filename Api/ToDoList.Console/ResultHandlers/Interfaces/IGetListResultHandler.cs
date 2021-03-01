using System.Collections.Generic;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;

namespace ToDoList.Console.ResultHandlers.Interfaces
{
    public interface IGetListResultHandler
    {
        void Handle(QueryResultWrapper<List<ListItemModel>> result);
    }
}