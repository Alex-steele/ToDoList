using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;

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

        public async Task<QueryResultWrapper<List<ListItemModel>>> ExecuteAsync()
        {
            var result = await repository.GetAllAsync();

            return QueryResultWrapper<List<ListItemModel>>.FromRepoResult(result,
                listItems => listItems.Select(x => mapper.Map(x)).ToList());
        }
    }
}
