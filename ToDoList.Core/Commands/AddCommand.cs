using System.Linq;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Validators.Enums;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IToDoListRepository repository;
        private readonly IUserInputValidator validator;

        public AddCommand(IToDoListRepository repository, IUserInputValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public void Execute(AddCommandModel model)
        {
            var validationResult = validator.Validate(model.ItemValue);

            if (validationResult == ValidationResult.Invalid)
            {
                return;
            }

            var listItems = repository.GetAll();

            if (listItems == null)
            {
                return;
            }

            repository.Add(new ListItem
            {
                Id = listItems.Max(x => x?.Id) + 1 ?? 1,
                Value = model.ItemValue,
                Completed = false
            });
        }
    }
}
