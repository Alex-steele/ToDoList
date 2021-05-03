using System;
using ToDoList.Core.Validators;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Core.Wrappers
{
    public class CommandResultWrapper
    {
        private CommandResultWrapper(CommandResult result)
        {
            Result = result;
        }

        private CommandResultWrapper(ListItem payload)
        {
            Result = CommandResult.Created;
            Payload = payload;
        }

        private CommandResultWrapper(ValidationResult validation)
        {
            Validation = validation;
            Result = CommandResult.ValidationError;
        }

        /// <summary>
        /// Create a new CommandResultWrapper as created
        /// </summary>
        public static CommandResultWrapper Created(ListItem payload) => new CommandResultWrapper(payload);


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


        /// <summary>
        /// Create a new CommandResultWrapper from the repo result
        /// </summary>
        /// <param name="repoResult">Result from repository</param>
        /// <returns></returns>
        public static CommandResultWrapper FromRepoResult(RepoResult repoResult)
        {
            return repoResult switch
            {
                RepoResult.Success => Success,
                RepoResult.NotFound => NotFound,
                RepoResult.Error => Error,
                _ => throw new ArgumentOutOfRangeException(nameof(repoResult))
            };
        }

        public CommandResult Result { get; }

        public ListItem Payload { get; }

        public ValidationResult Validation { get; }
    }
}
