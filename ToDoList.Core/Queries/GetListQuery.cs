using System.Linq;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Queries
{
    public class GetListQuery : IGetListQuery
    {
        private readonly IToDoListRepository repository;
        private readonly IListItemMapper mapper;

        public GetListQuery(IToDoListRepository repository, IListItemMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public QueryResultWrapper Execute()
        {
            var listItems = repository.GetAll();

            if (listItems == null)
            {
                return QueryResultWrapper.Error;
            }

            var listItemModels = listItems.Select(x => mapper.Map(x)).ToList();

            return QueryResultWrapper.Success(listItemModels);
        }
    }
}
