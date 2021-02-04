using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Repositories
{
    public class EFReadOnlyRepository : IReadOnlyRepository
    {
        private readonly ToDoListContext context;
        private readonly ILogger<EFReadOnlyRepository> logger;

        public EFReadOnlyRepository(ToDoListContext context, ILogger<EFReadOnlyRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<RepoResultWrapper<ListItem>> GetByIdAsync(int id)
        {
            try
            {
                logger.LogInformation("Connecting to the database");

                var result = await context.ListItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

                return result == null
                    ? RepoResultWrapper<ListItem>.NotFound()
                    : RepoResultWrapper<ListItem>.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying to connect to the database");
                return RepoResultWrapper<ListItem>.Error();
            }
        }

        public async Task<RepoResultWrapper<ListItem>> GetByIdForEditAsync(int id)
        {
            try
            {
                logger.LogInformation("Connecting to the database");

                var result = await context.ListItems.SingleOrDefaultAsync(x => x.Id == id);

                return result == null
                    ? RepoResultWrapper<ListItem>.NotFound()
                    : RepoResultWrapper<ListItem>.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying to connect to the database");
                return RepoResultWrapper<ListItem>.Error();
            }
        }

        public async Task<RepoResultWrapper<List<ListItem>>> GetAllAsync()
        {
            try
            {
                logger.LogInformation("Connecting to the database");

                var result = await context.ListItems.AsNoTracking().ToListAsync();

                return result == null
                    ? RepoResultWrapper<List<ListItem>>.Error()
                    : RepoResultWrapper<List<ListItem>>.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying to connect to the database");
                return RepoResultWrapper<List<ListItem>>.Error();
            }
        }
    }
}
