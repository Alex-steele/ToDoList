using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class CompleteCommand
    {
        private readonly IToDoListRepository repository;

        public CompleteCommand(IToDoListRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(int id)
        {
            // Validate input

            var item = repository.GetById(id);

            repository.Complete(item);
        }
    }
}
