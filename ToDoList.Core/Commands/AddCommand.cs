using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IWriteRepository writeRepository;
        private readonly IAddCommandValidator validator;

        public AddCommand(IWriteRepository writeRepository, IAddCommandValidator validator)
        {
            this.writeRepository = writeRepository;
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

            writeRepository.Add(new ListItem(model.ItemValue));
            var saveResult = await writeRepository.SaveChangesAsync();

            return CommandResultWrapper.FromRepoResult(saveResult.Result);
        }
    }
}
