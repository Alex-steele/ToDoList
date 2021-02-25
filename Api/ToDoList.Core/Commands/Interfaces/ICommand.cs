using System.Threading.Tasks;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Commands.Interfaces
{
    public interface ICommand<in T>
    {
        Task<CommandResultWrapper> ExecuteAsync(T model);
    }
}