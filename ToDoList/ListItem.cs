namespace ToDoList.Core
{
    public class ListItem
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public void Complete()
        {
            if (Completed)
            {
                return;
            }
            Completed = true;
            Value += " --Completed";
        }
    }
}
