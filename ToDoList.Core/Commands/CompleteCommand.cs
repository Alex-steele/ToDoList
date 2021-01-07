using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
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

        public CommandResultWrapper Execute(CompleteCommandModel model)
        {
            Check.NotNull(model, nameof(model));

            var item = repository.GetById(model.ItemId);

            if (item == null)
            {
                return CommandResultWrapper.NotFound;
            }

            repository.Complete(item);
            repository.SaveChanges();

            return CommandResultWrapper.Success;
        }
    }
}
