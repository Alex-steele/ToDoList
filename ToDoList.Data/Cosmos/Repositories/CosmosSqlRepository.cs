using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ToDoList.Data.Cosmos.Entities;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Data.Cosmos.Repositories
{
    public class CosmosSqlRepository : ICosmosRepository
    {
        private readonly ILogger<CosmosSqlRepository> logger;
        private readonly CosmosClient client;

        public CosmosSqlRepository(ILogger<CosmosSqlRepository> logger)
        {
            this.logger = logger;

            try
            {
                client = new CosmosClient(
                    "https://localhost:8081",
                    "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
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

                var sql = $"SELECT l['UserId'], l['IntId'], l['id'], l['Value'], l['Completed'] FROM ListItems l WHERE l['IntId'] = {id}";

                var iterator = container.GetItemQueryIterator<CosmosListItem>(sql);
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

                var sql = "SELECT l['IntId'], l['Value'], l['Completed'] FROM ListItems l";

                var iterator = container.GetItemQueryIterator<CosmosListItem>(sql);
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
