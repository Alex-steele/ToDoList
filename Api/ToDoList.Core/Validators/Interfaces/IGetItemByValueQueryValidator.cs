using ToDoList.Core.Models;

namespace ToDoList.Core.Validators.Interfaces
{
    public interface IGetItemByValueQueryValidator
    {
        ValidationResult Validate(GetItemByValueQueryModel model);
    }
}