namespace ToDoList.Core.Services.Queries
{
    public class QueryResultWrapper<T>
    {
        public T Payload { get; set; }

        public QueryResult Result { get; set; }
    }
}
