namespace ToDoList.Core
{
    public class ListItem
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public void Complete()
        {
            Completed = true;
            Value += " --Completed";
        }
    }
}
