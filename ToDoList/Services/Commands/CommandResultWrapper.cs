using FluentValidation.Results;

namespace ToDoList.Core.Services.Commands
{
    public class CommandResultWrapper<T>
    {
        public ValidationResult Validation { get; set; }

        public T Payload { get; set; }

        public CommandResult Result { get; set; }
    }
}
