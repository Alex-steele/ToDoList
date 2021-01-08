using ToDoList.Console.Installers.Interfaces;

namespace ToDoList.Console.Runner.Interface
{
    public interface IToDoListRunner
    {
        void Run(IToDoListServiceContainer serviceProvider, string[] args);
    }
}