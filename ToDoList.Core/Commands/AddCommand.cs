using System.Linq;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IToDoListRepository repository;
        private readonly IAddCommandValidator validator;

        public AddCommand(IToDoListRepository repository, IAddCommandValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public CommandResultWrapper Execute(AddCommandModel model)
        {
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return new CommandResultWrapper
                {
                    Result = CommandResult.ValidationError,
                    Validation = validationResult
                };
            }

            var listItems = repository.GetAll();

            if (listItems == null)
            {
                return new CommandResultWrapper
                {
                    Result = CommandResult.Error
                };
            }

            repository.Add(new ListItem
            {
                Id = listItems.Count() + 1,
                Value = model.ItemValue,
                Completed = false
            });

            return new CommandResultWrapper
            {
                Result = CommandResult.Success
            };
        }
    }
}
