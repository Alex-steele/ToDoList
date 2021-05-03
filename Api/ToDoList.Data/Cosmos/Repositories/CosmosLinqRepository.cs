using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Data.Configuration;
using ToDoList.Data.Cosmos.Entities;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Cosmos.Repositories
{
    public class CosmosLinqRepository : ICosmosRepository
    {
        private readonly ILogger<CosmosLinqRepository> logger;
        private readonly CosmosClient client;

        public CosmosLinqRepository(ILogger<CosmosLinqRepository> logger, ICosmosConfiguration config)
        {
            this.logger = logger;

            try
            {
                client = new CosmosClient(config.ConnectionString);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to connect to the database", ex);
            }
        }

        public async Task<RepoResultWrapper<Unit>> AddAsync(CosmosListItem item)
        {
            try
            {
                var container = client.GetContainer("ToDoListCosmosDB", "ListItems");

                await container.CreateItemAsync(item, new PartitionKey(item.UserId));

                return RepoResultWrapper<Unit>.Success(Unit.Default);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to add an item to the database", ex);
                return RepoResultWrapper<Unit>.Error();
            }
        }

        public async Task<RepoResultWrapper<Unit>> UpdateAsync(CosmosListItem item)
        {
            try
            {
                var container = client.GetContainer("ToDoListCosmosDB", "ListItems");

                await container.ReplaceItemAsync(item, item.id, new PartitionKey(item.UserId));

                return RepoResultWrapper<Unit>.Success(Unit.Default);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to update an item in the database", ex);
                return RepoResultWrapper<Unit>.Error();
            }
        }
        public async Task<RepoResultWrapper<CosmosListItem>> GetByIntIdAsync(int id)
        {
            try
            {
                var container = client.GetContainer("ToDoListCosmosDB", "ListItems");

                var linq = container.GetItemLinqQueryable<CosmosListItem>()
                    .Where(item => item.IntId == id);

                var iterator = linq.ToFeedIterator();
                var result = await iterator.ReadNextAsync();

                var listItem = result.ToList().SingleOrDefault();

                return listItem == null
                    ? RepoResultWrapper<CosmosListItem>.NotFound()
                    : RepoResultWrapper<CosmosListItem>.Success(listItem);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to get an item from the database", ex);
                return RepoResultWrapper<CosmosListItem>.Error();
            }
        }

        public async Task<RepoResultWrapper<List<CosmosListItem>>> GetAllAsync()
        {
            try
            {
                var listItems = new List<CosmosListItem>();

                var container = client.GetContainer("ToDoListCosmosDB", "ListItems");

                var linq = container.GetItemLinqQueryable<CosmosListItem>();

                var iterator = linq.ToFeedIterator();
                var result = await iterator.ReadNextAsync();

                listItems.AddRange(result);

                return RepoResultWrapper<List<CosmosListItem>>.Success(listItems);
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while trying to get items from the database", ex);
                return RepoResultWrapper<List<CosmosListItem>>.Error();
            }
        }

    }
}
