using System;
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
            Check.NotNull(model, nameof(model));

            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return CommandResultWrapper.ValidationError(validationResult);
            }

            repository.Add(new ListItem(model.ItemValue));

            return CommandResultWrapper.Success;
        }
    }
}
