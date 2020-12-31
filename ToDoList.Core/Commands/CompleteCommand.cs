using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class CompleteCommand : ICompleteCommand
    {
        private readonly IToDoListRepository repository;

        public CompleteCommand(IToDoListRepository repository)
        {
            this.repository = repository;
        }

        public CommandResult Execute(CompleteCommandModel model)
        {
            var item = repository.GetById(model.ItemId);

            if (item == null)
            {
                return CommandResult.NotFound;
            }

            repository.Complete(item);

            return CommandResult.Success;
        }
    }
}
