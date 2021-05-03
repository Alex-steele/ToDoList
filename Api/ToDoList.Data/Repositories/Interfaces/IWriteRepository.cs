using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IWriteRepository
    {
        void Add(ListItem item);

        void Update(ListItem item);

        void Delete(ListItem item);

        Task<RepoResultWrapper<Unit>> SaveChangesAsync();
    }
}
