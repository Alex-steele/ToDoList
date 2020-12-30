using System;

namespace ToDoList.Console.Installers.Interfaces
{
    public interface IToDoListServiceContainer : IDisposable
    {
        T GetService<T>();
    }
}
