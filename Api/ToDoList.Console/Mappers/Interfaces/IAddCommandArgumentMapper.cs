using ToDoList.Console.Arguments;
using ToDoList.Core.Models;

namespace ToDoList.Console.Mappers.Interfaces
{
    public interface IAddCommandArgumentMapper
    {
        AddCommandModel Map(AddCommandArguments args);
    }
}