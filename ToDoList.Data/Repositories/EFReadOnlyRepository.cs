using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories
{
    public class EFReadOnlyRepository : IReadOnlyRepository
    {
        private readonly ToDoListContext context;

        public EFReadOnlyRepository(ToDoListContext context)
        {
            this.context = context;
        }

        public async Task<RepoResultWrapper<ListItem>> GetByIdAsync(int id)
        {
            var result = await context.ListItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            return result == null
                ? RepoResultWrapper<ListItem>.NotFound()
                : RepoResultWrapper<ListItem>.Success(result);
        }

        public async Task<RepoResultWrapper<List<ListItem>>> GetAllAsync()
        {
            var result = await context.ListItems.AsNoTracking().ToListAsync();

            return result == null
                ? RepoResultWrapper<List<ListItem>>.Error()
                : RepoResultWrapper<List<ListItem>>.Success(result);
        }
    }
}
