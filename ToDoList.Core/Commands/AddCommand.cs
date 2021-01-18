using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Utilities;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IWriteRepository repository;
        private readonly IAddCommandValidator validator;

        public AddCommand(IWriteRepository repository, IAddCommandValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<CommandResultWrapper> ExecuteAsync(AddCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return CommandResultWrapper.ValidationError(validationResult);
            }

            repository.Add(new ListItem(model.ItemValue));
            await repository.SaveChangesAsync();

            return CommandResultWrapper.Success;
        }
    }
}
