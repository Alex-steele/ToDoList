using ToDoList.Core.Models;

namespace ToDoList.Core.Commands.Interfaces
{
    public interface ICompleteCommand : ICommand<CompleteCommandModel>
    {
    }
}