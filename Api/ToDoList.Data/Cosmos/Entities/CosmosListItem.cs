namespace ToDoList.Data.Cosmos.Entities
{
    public class CosmosListItem
    {
        public string UserId { get; set; }

        public string Value { get; set; }

        public bool Completed { get; set; }

        public int IntId { get; set; }

        public string id { get; set; }
    }
}