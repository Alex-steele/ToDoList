using System;

namespace ToDoList.Data.Entities
{
    public class ListItem
    {
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
            Date = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public DateTime Date { get; set; }

        public void Complete()
        {
            Completed = true;
        }
    }
}
