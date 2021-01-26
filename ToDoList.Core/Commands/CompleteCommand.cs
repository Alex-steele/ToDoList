using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Commands
{
    public class CompleteCommand : ICompleteCommand
    {
        private readonly IWriteRepository writeRepository;
        private readonly IReadOnlyRepository readRepository;

        public CompleteCommand(IWriteRepository writeRepository, IReadOnlyRepository readRepository)
        {
            this.writeRepository = writeRepository;
            this.readRepository = readRepository;
        }

        public async Task<CommandResultWrapper> ExecuteAsync(CompleteCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var result = await readRepository.GetByIdForEditAsync(model.ItemId);

            if (result.Result != RepoResult.Success)
            {
                return CommandResultWrapper.FromRepoResult(result.Result);
            }

            result.Payload.Complete();

            writeRepository.Update(result.Payload);
            var saveResult = await writeRepository.SaveChangesAsync();

            return CommandResultWrapper.FromRepoResult(saveResult.Result);
        }
    }
}
