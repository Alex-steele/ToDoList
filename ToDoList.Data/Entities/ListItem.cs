using System;

namespace ToDoList.Data.Entities
{
    public class ListItem
    {
        // Can be protected if using EF
        public ListItem()
        {
        }

        public ListItem(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
            Completed = false;
        }

        // Setters can be protected if using EF
        public int Id { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public void Complete()
        {
            Completed = true;
        }
    }
}
