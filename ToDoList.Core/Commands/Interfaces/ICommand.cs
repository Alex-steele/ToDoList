using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Commands.Interfaces
{
    public interface ICommand<in T>
    {
        CommandResultWrapper Execute(T model);
    }
}