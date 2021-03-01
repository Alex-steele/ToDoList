using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Cosmos.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Core.Cosmos.Queries.EF
{
    public class GetListQueryCosmosEF : IGetListQuery
    {
        private readonly ICosmosEFRepository repository;
        private readonly ICosmosListItemMapper mapper;

        public GetListQueryCosmosEF(ICosmosEFRepository repository, ICosmosListItemMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<QueryResultWrapper<List<ListItemModel>>> ExecuteAsync()
        {
            var result = await repository.GetAllAsync();

            if (result.Result == RepoResult.Error)
            {
                return QueryResultWrapper<List<ListItemModel>>.Error;
            }

            var listItemModels = result.Payload.Select(x => mapper.Map(x)).ToList();

            return QueryResultWrapper<List<ListItemModel>>.Success(listItemModels);
        }
    }
}
