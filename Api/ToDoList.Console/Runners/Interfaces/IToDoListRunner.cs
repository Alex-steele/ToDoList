using System.Threading.Tasks;

namespace ToDoList.Console.Runners.Interfaces
{
    public interface IToDoListRunner
    {
        void Run(string[] args);
    }
}