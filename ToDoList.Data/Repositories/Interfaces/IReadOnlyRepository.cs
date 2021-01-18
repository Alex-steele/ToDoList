using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IReadOnlyRepository
    {
        Task<RepoResultWrapper<ListItem>> GetByIdAsync(int id);

        Task<RepoResultWrapper<ListItem>> GetByIdForEditAsync(int id);

        Task<RepoResultWrapper<List<ListItem>>> GetAllAsync();
    }
}
