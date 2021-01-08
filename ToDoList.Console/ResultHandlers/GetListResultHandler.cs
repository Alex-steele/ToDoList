using ToDoList.Console.Messages;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console.ResultHandlers
{
    public static class GetListResultHandler
    {
        public static void Handle(QueryResultWrapper result)
        {
            switch (result.Result)
            {
                case QueryResult.Success:
                    DisplayList.Display(result.Payload);
                    break;

                case QueryResult.Error:
                    ErrorMessage.Write($"Could not retrieve list");
                    break;
            }
        }
    }
}
