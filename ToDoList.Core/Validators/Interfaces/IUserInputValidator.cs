using ToDoList.Core.Models;

namespace ToDoList.Core.Validators.Interfaces
{
    public interface IUserInputValidator
    {
        ValidationResult Validate(AddCommandModel model);
    }
}