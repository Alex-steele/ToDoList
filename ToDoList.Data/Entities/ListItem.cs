using System;

namespace ToDoList.Data.Entities
{
    public class ListItem
    {
        // Change back to protected
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
        // Change setter back to protected
        public int Id { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public void CompleteItem()
        {
            Completed = true;
        }
    }
}
