using System;
using System.Collections.Generic;
using ToDoList.Console.Messages;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console.ResultHandlers
{
    public class GetListResultHandler : IGetListResultHandler
    {
        public void Handle(QueryResultWrapper<List<ListItemModel>> result)
        {
            switch (result.Result)
            {
                case QueryResult.Success:
                    DisplayList.Display(result.Payload);
                    break;

                case QueryResult.Error:
                    ErrorMessage.Write("Could not retrieve list");
                    break;
               
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
