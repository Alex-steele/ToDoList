using ToDoList.Core.Validators;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Core.Wrappers
{
    public class CommandResultWrapper
    {
        public CommandResult Result { get; set; }
        public ValidationResult Validation { get; set; }
    }
}
