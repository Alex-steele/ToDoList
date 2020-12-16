using ToDoList.Core.Validators.Enums;

namespace ToDoList.Core.Validators.Interfaces
{
    public interface IUserInputValidator
    {
        ValidationResult Validate(string input);
    }
}