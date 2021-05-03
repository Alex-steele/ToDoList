using ToDoList.Core.Models;

namespace ToDoList.Core.Validators.Interfaces
{
    public interface IAddCommandValidator
    {
        ValidationResult Validate(AddCommandModel model);
    }
}