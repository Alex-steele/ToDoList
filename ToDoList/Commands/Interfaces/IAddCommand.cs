using System.Reactive;
using ToDoList.Core.Services.Commands;

namespace ToDoList.Core.Commands.Interfaces
{
    public interface IAddCommand : ICommand<string, Unit>
    {
    }
}
