using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Cosmos.Commands.EF
{
    public class CompleteCommandCosmosEF : ICompleteCommand
    {
        private readonly ICosmosEFRepository repository;

        public CompleteCommandCosmosEF(ICosmosEFRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResultWrapper> ExecuteAsync(CompleteCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var result = await repository.GetByIntIdAsync(model.ItemId);

            switch (result.Result)
            {
                case RepoResult.NotFound:
                    return CommandResultWrapper.NotFound;

                case RepoResult.Error:
                    return CommandResultWrapper.Error;
            }

            result.Payload.Completed = true;

            repository.Update(result.Payload);
            var saveResult = await repository.SaveChangesAsync();

            return saveResult.Result == RepoResult.Error
                ? CommandResultWrapper.Error
                : CommandResultWrapper.Success;
        }
    }
}

