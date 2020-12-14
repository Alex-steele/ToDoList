using System.Reactive;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Services.Commands;
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

        public CommandResultWrapper<Unit> Execute(int id)
        {
            // Validate input

            var item = repository.GetById(id);

            repository.Complete(item);

            return new CommandResultWrapper<Unit>();
        }
    }
}
