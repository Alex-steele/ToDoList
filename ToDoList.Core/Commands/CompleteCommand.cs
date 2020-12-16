using ToDoList.Core.Commands.Interfaces;
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

        public void CompleteItem(int id)
        {
            var item = repository.GetById(id);
            repository.Complete(item);
        }
    }
}
