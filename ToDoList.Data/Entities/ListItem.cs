using System;

namespace ToDoList.Data.Entities
{
    public class ListItem
    {
        protected ListItem()
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

        public int Id { get; protected set; }

        public string Value { get; protected set; }

        public bool Completed { get; protected set; }

        public void CompleteItem()
        {
            Completed = true;
        }
    }
}
