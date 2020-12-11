using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly List<ToDoListItem> _toDoListItems = new List<ToDoListItem>();

        public void Add(ToDoListItem item)
        {
            _toDoListItems.Add(item);
            item.Id = _toDoListItems.Max(x => x?.Id) + 1 ?? 1;
        }

        public void Complete(ToDoListItem item)
        {
            item.Completed = true;
        }

        public ToDoListItem GetById(int id)
        {
            return _toDoListItems.Single(x => x.Id == id);
        }
    }
}
