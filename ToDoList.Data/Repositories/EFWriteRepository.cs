using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Repositories
{
    public class EFWriteRepository : IWriteRepository
    {
        private readonly ToDoListContext context;

        public EFWriteRepository(ToDoListContext context)
        {
            this.context = context;
        }

        public void Add(ListItem item)
        {
            context.ListItems.Add(item);
        }

        public void Update(ListItem item)
        {
            context.ListItems.Update(item);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
