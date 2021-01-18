using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Core.Queries
{
    public class GetListQuery : IGetListQuery
    {
        private readonly IReadOnlyRepository repository;
        private readonly IListItemMapper mapper;

        public GetListQuery(IReadOnlyRepository repository, IListItemMapper mapper)
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
