using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListContext context;

        public ToDoListRepository(ToDoListContext context)
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

        public async Task<RepoResultWrapper<ListItem>> GetByIdAsync(int id)
        {
            var result = await context.ListItems.SingleOrDefaultAsync(x => x.Id == id);

            return result == null
                ? RepoResultWrapper<ListItem>.NotFound()
                : RepoResultWrapper<ListItem>.Success(result);
        }

        public async Task<RepoResultWrapper<List<ListItem>>> GetAllAsync()
        {
            var result = await context.ListItems.ToListAsync();

            return result == null
                ? RepoResultWrapper<List<ListItem>>.Error()
                : RepoResultWrapper<List<ListItem>>.Success(result);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}