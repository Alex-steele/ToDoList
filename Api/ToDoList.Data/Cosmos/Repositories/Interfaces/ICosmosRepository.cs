using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Data.Cosmos.Entities;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Cosmos.Repositories.Interfaces
{
    public interface ICosmosRepository
    {
        Task<RepoResultWrapper<Unit>> AddAsync(CosmosListItem item);

        Task<RepoResultWrapper<Unit>> UpdateAsync(CosmosListItem item);

        Task<RepoResultWrapper<CosmosListItem>> GetByIntIdAsync(int id);

        Task<RepoResultWrapper<List<CosmosListItem>>> GetAllAsync();
    }
}
