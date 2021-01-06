using ToDoList.Core.Validators;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Core.Wrappers
{
    public class CommandResultWrapper
    {
        private CommandResultWrapper(CommandResult result)
        {
            Result = result;
        }

        private CommandResultWrapper(ValidationResult validation)
        {
            Validation = validation;
            Result = CommandResult.ValidationError;
        }

        /// <summary>
        /// Create a new CommandResultWrapper as success
        /// </summary>
        public static CommandResultWrapper Success => new CommandResultWrapper(CommandResult.Success);

        /// <summary>
        /// Create a new CommandResultWrapper as not found
        /// </summary>
        public static CommandResultWrapper NotFound => new CommandResultWrapper(CommandResult.NotFound);

        /// <summary>
        /// Create a new CommandResultWrapper as error
        /// </summary>
        public static CommandResultWrapper Error => new CommandResultWrapper(CommandResult.Error);

        /// <summary>
        /// Create a new CommandResultWrapper as validation error
        /// </summary>
        /// <param name="validation">Validation errors to include</param>
        /// <returns></returns>
        public static CommandResultWrapper ValidationError(ValidationResult validation) => new CommandResultWrapper(validation);

        public CommandResult Result { get; }
        public ValidationResult Validation { get; }
    }
}
