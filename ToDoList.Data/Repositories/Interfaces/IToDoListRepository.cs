using System.Collections.Generic;
using ToDoList.Data.Entities;

namespace ToDoList.Data.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        void Add(ListItem item);

        void Complete(ListItem item);

        ListItem GetById(int id);

        List<ListItem> GetAll();

        void SaveChanges();
    }
}