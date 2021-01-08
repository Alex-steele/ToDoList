using ToDoList.Console.Arguments;
using ToDoList.Console.Messages;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console.ResultHandlers
{
    public static class CompleteResultHandler
    {
        public static void Handle(CommandResultWrapper result, CompleteCommandArguments arguments)
        {
            switch (result.Result)
            {
                case CommandResult.Success:
                    SuccessMessage.Write("Item successfully completed");
                    break;

                case CommandResult.NotFound:
                    ErrorMessage.Write($"Could not find item with specified Id: {arguments.ItemId}");
                    break;
            }
        }
    }
}
