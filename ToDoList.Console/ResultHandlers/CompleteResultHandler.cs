using System;
using ToDoList.Console.Arguments;
using ToDoList.Console.Messages;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console.ResultHandlers
{
    public class CompleteResultHandler : ICompleteResultHandler
    {
        public void Handle(CommandResultWrapper result, CompleteCommandArguments arguments)
        {
            switch (result.Result)
            {
                case CommandResult.Success:
                    SuccessMessage.Write("Item successfully completed");
                    break;

                case CommandResult.NotFound:
                    ErrorMessage.Write($"Could not find item with specified Id: {arguments.ItemId}");
                    break;

                case CommandResult.Error:
                    ErrorMessage.Write("An error occurred while executing the complete command");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
