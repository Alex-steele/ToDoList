using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Utilities;

namespace ToDoList.Core.Queries
{
    public class GetItemByIdQuery : IGetItemByIdQuery
    {
        private readonly IReadOnlyRepository repository;
        private readonly IListItemMapper mapper;

        public GetItemByIdQuery(IReadOnlyRepository repository, IListItemMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<QueryResultWrapper> ExecuteAsync(GetItemByIdQueryModel model)
        {
            Check.NotNull(model, nameof(model));

            var result = await repository.GetByIdAsync(model.ItemId);

            return QueryResultWrapper.FromRepoResult(result,
                listItem => new List<ListItemModel> { mapper.Map(listItem) });
        }

    }
}
