using System.Reactive;
using ToDoList.Core.Services.Commands;

namespace ToDoList.Core.Commands.Interfaces
{
    interface ICompleteCommand : ICommand<int, Unit>
    {
    }
}
