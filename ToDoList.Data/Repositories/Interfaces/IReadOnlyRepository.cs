using System;
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

        Task<RepoResultWrapper<IEnumerable<ListItem>>> GetByValueAsync(string itemValue);

        Task<RepoResultWrapper<IEnumerable<ListItem>>> GetByValueFuzzyAsync(string itemValue);

        Task<RepoResultWrapper<IEnumerable<ListItem>>> GetByDateAsync(DateTime date);

        Task<RepoResultWrapper<IEnumerable<ListItem>>> GetAllAsync();
    }
}
