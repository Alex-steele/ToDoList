using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class CompleteCommand
    {
        private readonly IToDoListRepository _repository;

        public CompleteCommand(IToDoListRepository repository)
        {
            _repository = repository;
        }

        public void Execute(int id)
        {
            // Validate input

            var item = _repository.GetById(id);
            _repository.Complete(item);
        }
    }
}
