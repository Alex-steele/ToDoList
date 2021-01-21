using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Cosmos.Mappers.Interfaces;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Cosmos.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Core.Cosmos.Queries
{
    public class GetListQueryCosmos : IGetListQuery
    {
        private readonly ICosmosRepository repository;
        private readonly ICosmosListItemMapper mapper;

        public GetListQueryCosmos(ICosmosRepository repository, ICosmosListItemMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<QueryResultWrapper> ExecuteAsync()
        {
            var result = await repository.GetAllAsync();

            if (result.Result == RepoResult.Error)
            {
                return QueryResultWrapper.Error;
            }

            var listItemModels = result.Payload.Select(x => mapper.Map(x)).ToList();

            return QueryResultWrapper.Success(listItemModels);
        }
    }
}
