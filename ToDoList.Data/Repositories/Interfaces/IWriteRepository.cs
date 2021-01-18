using System.Threading.Tasks;
using ToDoList.Data.Entities;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IWriteRepository
    {
        void Add(ListItem item);

        void Update(ListItem item);

        Task SaveChangesAsync();
    }
}
