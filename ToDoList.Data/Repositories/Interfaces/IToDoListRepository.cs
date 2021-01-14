using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        void Add(ListItem item);

        void Update(ListItem item);

        Task<RepoResultWrapper<ListItem>> GetByIdAsync(int id);

        Task<RepoResultWrapper<List<ListItem>>> GetAllAsync();

        Task SaveChangesAsync();
    }
}