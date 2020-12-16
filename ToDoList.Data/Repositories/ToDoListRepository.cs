using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly List<ListItem> listItems = new List<ListItem>();

        public void Add(ListItem item)
        {
            listItems.Add(item);
        }

        public void Complete(ListItem item)
        {
            item.Completed = true;
        }

        public ListItem GetById(int id)
        {
            return listItems.Single(x => x.Id == id);
        }

        public List<ListItem> GetAll()
        {
            return listItems;
        }
    }
}
