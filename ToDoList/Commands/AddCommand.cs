using System.Reactive;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Services.Commands;
using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IToDoListRepository repository;

        public AddCommand(IToDoListRepository repository)
        {
            this.repository = repository;
        }

        public CommandResultWrapper<Unit> Execute(string itemValue)
        {
            // Validate input

            repository.Add(new ToDoListItem
            {
                Value = itemValue,
                Completed = false
            });

            return new CommandResultWrapper<Unit>();
        }
    }
}