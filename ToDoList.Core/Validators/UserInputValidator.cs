using ToDoList.Core.Validators.Enums;
using ToDoList.Core.Validators.Interfaces;

namespace ToDoList.Core.Validators
{
    public class UserInputValidator : IUserInputValidator
    {
        public ValidationResult Validate(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? ValidationResult.Invalid : ValidationResult.Valid;
        }
    }
}
