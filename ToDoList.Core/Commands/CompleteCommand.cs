using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
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

        public CommandResultWrapper Execute(CompleteCommandModel model)
        {
            var item = repository.GetById(model.ItemId);

            if (item == null)
            {
                return new CommandResultWrapper
                {
                    Result = CommandResult.NotFound
                };
            }

            repository.Complete(item);

            return new CommandResultWrapper
            {
                Result = CommandResult.Success
            };
        }
    }
}
