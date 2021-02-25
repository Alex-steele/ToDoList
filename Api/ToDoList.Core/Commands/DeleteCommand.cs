using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Commands
{
    public class DeleteCommand : IDeleteCommand
    {
        private readonly IWriteRepository writeRepository;
        private readonly IReadOnlyRepository readRepository;

        public DeleteCommand(IWriteRepository writeRepository, IReadOnlyRepository readRepository)
        {
            this.writeRepository = writeRepository;
            this.readRepository = readRepository;
        }

        public async Task<CommandResultWrapper> ExecuteAsync(DeleteCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var result = await readRepository.GetByIdForEditAsync(model.ItemId);

            if (result.Result != RepoResult.Success)
            {
                return CommandResultWrapper.FromRepoResult(result.Result);
            }

            writeRepository.Delete(result.Payload);
            var saveResult = await writeRepository.SaveChangesAsync();

            return CommandResultWrapper.FromRepoResult(saveResult.Result);
        }
    }
}
