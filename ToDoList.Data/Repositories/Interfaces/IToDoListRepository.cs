using System.Threading.Tasks;
using ToDoList.Data.Models;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        void Add(ToDoListItem itemValue);

        void Complete(ToDoListItem item);

        ToDoListItem GetById(int id)
    }
}