using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Utilities;

namespace ToDoList.Core.Queries
{
    public class GetItemByValueQuery : IGetItemByValueQuery
    {
        private readonly IGetItemByValueQueryValidator validator;
        private readonly IReadOnlyRepository repository;
        private readonly IListItemMapper mapper;

        public GetItemByValueQuery(IGetItemByValueQueryValidator validator,
            IReadOnlyRepository repository,
            IListItemMapper mapper)
        {
            this.validator = validator;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<QueryResultWrapper<List<ListItemModel>>> ExecuteAsync(GetItemByValueQueryModel model)
        {
            Check.NotNull(model, nameof(model));

            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return QueryResultWrapper<List<ListItemModel>>.ValidationError(validationResult);
            }

            var result = await repository.GetByValueAsync(model.ItemValue);

            return QueryResultWrapper<List<ListItemModel>>.FromRepoResult(result,
                listItems => listItems.Select(x => mapper.Map(x)).ToList());
        }
    }
}
