using System.Collections.Generic;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Queries
{
    public class GetListQuery : IGetListQuery
    {
        private readonly IToDoListRepository repository;

        public GetListQuery(IToDoListRepository repository)
        {
            this.repository = repository;
        }

        public List<ListItem> GetList()
        {
            return repository.GetAll();
        }
    }
}
