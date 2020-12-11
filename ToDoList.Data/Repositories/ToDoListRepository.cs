using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly List<ToDoListItem> toDoListItems = new List<ToDoListItem>();

        public void Add(ToDoListItem item)
        {
            toDoListItems.Add(item);
            item.Id = toDoListItems.Max(x => x?.Id) + 1 ?? 1;
        }

        public void Complete(ToDoListItem item)
        {
            item.Completed = true;
        }

        public ToDoListItem GetById(int id)
        {
            return toDoListItems.Single(x => x.Id == id);
        }
    }
}
