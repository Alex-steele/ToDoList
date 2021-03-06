using System;
using ToDoList.Console.Messages;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Console.ResultHandlers
{
    public class AddResultHandler : IAddResultHandler
    {
        public void Handle(CommandResultWrapper result)
        {
            switch (result.Result)
            {
                case CommandResult.Success:
                    SuccessMessage.Write("Item successfully added");
                    break;

                case CommandResult.ValidationError:
                    ValidationErrorMessage.Write(result.Validation);
                    break;

                case CommandResult.Error:
                    ErrorMessage.Write("An error occurred while executing the add command");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
