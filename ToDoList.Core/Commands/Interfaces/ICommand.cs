using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Core.Commands.Interfaces
{
    public interface ICommand<in T>
    {
        CommandResult Execute(T input);
    }
}