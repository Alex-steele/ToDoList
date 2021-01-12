﻿using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListContext context;

        public ToDoListRepository(ToDoListContext context)
        {
            this.context = context;
        }

        public void Add(ListItem item)
        {
            context.ListItems.Add(item);
        }

        public void Complete(ListItem item)
        {
            item.CompleteItem();
        }

        public ListItem GetById(int id)
        {
            return context.ListItems.SingleOrDefault(x => x.Id == id);
        }

        public List<ListItem> GetAll()
        {
            return context.ListItems.ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
