namespace ToDoList.WebAPI.Middleware
{
    public interface ISha256Generator
    {
        string ComputeSha256Hash(string rawData);
    }
}