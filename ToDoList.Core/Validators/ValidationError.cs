using System;

namespace ToDoList.Core.Validators
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string error)
        {
            Check.NotEmptyOrWhiteSpace(propertyName, nameof(propertyName));
            Check.NotEmptyOrWhiteSpace(error, nameof(error));

            Error = error;
            PropertyName = propertyName;
        }

        public string Error { get; }

        public string PropertyName { get; }
    }
   
}
