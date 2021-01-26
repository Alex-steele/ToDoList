using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Cosmos.Commands
{
    public class CompleteCommandCosmos : ICompleteCommand
    {
        private readonly ICosmosRepository repository;

        public CompleteCommandCosmos(ICosmosRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResultWrapper> ExecuteAsync(CompleteCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var result = await repository.GetByIntIdAsync(model.ItemId);

            if (result.Result != RepoResult.Success)
            {
                return CommandResultWrapper.AsResult(result.Result);
            }

            result.Payload.Completed = true;

            var updateResult = await repository.UpdateAsync(result.Payload);

            return CommandResultWrapper.AsResult(updateResult.Result);
        }
    }
}