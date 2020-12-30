using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
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

        public void Execute(CompleteCommandModel model)
        {
            var item = repository.GetById(model.ItemId);
            repository.Complete(item);
        }
    }
}
