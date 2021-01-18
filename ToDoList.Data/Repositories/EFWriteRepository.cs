using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

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

        public async Task<RepoResultWrapper<ListItem>> GetByIdForEditAsync(int id)
        {
            var result = await context.ListItems.SingleOrDefaultAsync(x => x.Id == id);

            return result == null
                ? RepoResultWrapper<ListItem>.NotFound()
                : RepoResultWrapper<ListItem>.Success(result);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
