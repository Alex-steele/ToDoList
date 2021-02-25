using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Data.Cosmos.Entities;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Cosmos.Repositories
{
    public class CosmosEFRepository : ICosmosEFRepository
    {
        private readonly ToDoListCosmosContext context;
        private readonly ILogger<CosmosEFRepository> logger;

        public CosmosEFRepository(ToDoListCosmosContext context, ILogger<CosmosEFRepository> logger)
        {
            this.context = context;
            this.logger = logger;

            //await context.Database.EnsureCreatedAsync();
        }

        public void Add(CosmosListItem item)
        {
            context.ListItems.Add(item);
        }

        public void Update(CosmosListItem item)
        {
            context.ListItems.Update(item);
        }

        public async Task<RepoResultWrapper<CosmosListItem>> GetByIntIdAsync(int id)
        {
            try
            {
                var result = await context.ListItems.SingleOrDefaultAsync(x => x.IntId == id);

                return result == null
                    ? RepoResultWrapper<CosmosListItem>.NotFound()
                    : RepoResultWrapper<CosmosListItem>.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to connect to the database", ex);
                return RepoResultWrapper<CosmosListItem>.Error();
            }
        }

        public async Task<RepoResultWrapper<List<CosmosListItem>>> GetAllAsync()
        {
            try
            {
                var result = await context.ListItems.ToListAsync();

                return result == null
                    ? RepoResultWrapper<List<CosmosListItem>>.Error()
                    : RepoResultWrapper<List<CosmosListItem>>.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to connect to the database", ex);
                return RepoResultWrapper<List<CosmosListItem>>.Error();
            }
        }

        public async Task<RepoResultWrapper<Unit>> SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                return RepoResultWrapper<Unit>.Success(Unit.Default);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to connect to the database", ex);
                return RepoResultWrapper<Unit>.Error();
            }
        }
    }
}
