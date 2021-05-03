using System;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories
{
    public class EFWriteRepository : IWriteRepository
    {
        private readonly ToDoListContext context;
        private readonly ILogger<EFWriteRepository> logger;

        public EFWriteRepository(ToDoListContext context, ILogger<EFWriteRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Add(ListItem item)
        {
            context.ListItems.Add(item);
        }

        public void Update(ListItem item)
        {
            context.ListItems.Update(item);
        }

        public void Delete(ListItem item)
        {
            context.ListItems.Remove(item);
        }

        public async Task<RepoResultWrapper<Unit>> SaveChangesAsync()
        {
            try
            {
                logger.LogInformation("Connecting to the database");

                await context.SaveChangesAsync();
                return RepoResultWrapper<Unit>.Success(Unit.Default);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying to connect to the database");
                return RepoResultWrapper<Unit>.Error();
            }
        }
    }
}
