using ToDoList.Utilities;

namespace ToDoList.Core.Validators
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string errorMessage)
        {
            Check.NotNullOrWhiteSpace(propertyName, nameof(propertyName));
            Check.NotNullOrWhiteSpace(errorMessage, nameof(errorMessage));

            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public string ErrorMessage { get; }

        public string PropertyName { get; }
    }

}
