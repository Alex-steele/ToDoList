using System;

namespace ToDoList.Core.Models
{
    public class ListItemModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public DateTime Date { get; set; }
    }
}
