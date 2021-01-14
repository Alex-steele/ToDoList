using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Core.Commands
{
    public class CompleteCommand : ICompleteCommand
    {
        private readonly IToDoListRepository repository;

        public CompleteCommand(IToDoListRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResultWrapper> ExecuteAsync(CompleteCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var result = await repository.GetByIdAsync(model.ItemId);

            if (result.Result == RepoResult.NotFound)
            {
                return CommandResultWrapper.NotFound;
            }

            result.Payload.Complete();

            repository.Update(result.Payload);
            await repository.SaveChangesAsync();

            return CommandResultWrapper.Success;
        }
    }
}
