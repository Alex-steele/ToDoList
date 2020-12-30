namespace ToDoList.Core.Commands.Interfaces
{
    public interface ICommand<in T>
    {
        void Execute(T input);
    }
}