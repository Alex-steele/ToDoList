namespace ToDoList.Console.Installers.Interfaces
{
    public interface IToDoListServiceContainer
    {
        T GetService<T>();
    }
}
