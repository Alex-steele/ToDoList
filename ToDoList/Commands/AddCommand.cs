using ToDoList.Core.Commands.Interfaces;
using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IToDoListRepository _repository;

        public AddCommand(IToDoListRepository repository)
        {
            _repository = repository;
        }

        public void Execute(string itemValue)
        {
            // Validate input

            _repository.Add(new ToDoListItem
            {
                Value = itemValue,
                Completed = false
            });
        }
    }
}