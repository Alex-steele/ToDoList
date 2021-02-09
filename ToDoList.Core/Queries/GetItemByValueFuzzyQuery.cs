using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers.Enums;
using ToDoList.Utilities;

namespace ToDoList.Core.Queries
{
    public class GetItemByValueFuzzyQuery : IGetItemByValueFuzzyQuery
    {
        private readonly IReadOnlyRepository repository;
        private readonly IListItemMapper mapper;

        public GetItemByValueFuzzyQuery(IReadOnlyRepository repository, IListItemMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<QueryResultWrapper> ExecuteAsync(GetItemByValueQueryModel model)
        {
            Check.NotNull(model, nameof(model));

            model.ItemValue ??= string.Empty;

            var result = await repository.GetByValueFuzzyAsync(model.ItemValue);

            switch (result.Result)
            {
                case RepoResult.Error:
                    return QueryResultWrapper.Error;

                case RepoResult.NotFound:
                    return QueryResultWrapper.NotFound;
            }

            var listItemModels = result.Payload.Select(x => mapper.Map(x)).ToList();

            return QueryResultWrapper.Success(listItemModels);
        }
    }
}