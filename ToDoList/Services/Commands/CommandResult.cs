namespace ToDoList.Core.Services.Commands
{
    public enum CommandResult
    {
        Success = 0,
        NotFound = 1,
        InvalidOperation = 2,
        ValidationError = 3
    }
}
