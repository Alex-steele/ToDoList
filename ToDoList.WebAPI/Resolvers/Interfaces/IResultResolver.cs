using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebAPI.Resolvers.Interfaces
{
    public interface IResultResolver<in T>
    {
        IActionResult Resolve(T result);
    }
}