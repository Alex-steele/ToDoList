using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Commands.Interfaces
{
    public interface IDeleteCommand : ICommand<DeleteCommandModel>
    {
    }
}
