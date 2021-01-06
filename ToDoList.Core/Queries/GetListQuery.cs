using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
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

        public List<ListItemModel> GetList()
        {
            var listItems = repository.GetAll();
            var listItemModels = listItems.Select(x => mapper.Map(x)).ToList();
            return listItemModels;
        }
    }
}
