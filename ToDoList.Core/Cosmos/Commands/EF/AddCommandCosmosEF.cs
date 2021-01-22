using System;
using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Cosmos.Entities;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Cosmos.Commands.EF
{
    public class AddCommandCosmosEF : IAddCommand
    {
        private readonly ICosmosEFRepository repository;
        private readonly IAddCommandValidator validator;

        public AddCommandCosmosEF(ICosmosEFRepository repository, IAddCommandValidator validator)
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

            repository.Add(new CosmosListItem
            {
                UserId = "1",
                Value = model.ItemValue,
                Completed = false,
                IntId = new Random().Next(0, 1000000),
                id = Guid.NewGuid().ToString()
            });

            var saveResult = await repository.SaveChangesAsync();

            return saveResult.Result == RepoResult.Error
                ? CommandResultWrapper.Error
                : CommandResultWrapper.Success;
        }
    }
}
