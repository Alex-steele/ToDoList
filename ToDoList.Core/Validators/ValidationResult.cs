using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Utilities;

namespace ToDoList.Core.Validators
{
    public class ValidationResult : IValidationResult
    {
        private ValidationResult()
        {
            IsValid = true;
            Errors = new List<ValidationError>();
        }

        private ValidationResult(IEnumerable<ValidationError> errors)
        {
            Check.NotNullOrEmpty(errors, nameof(errors));

            IsValid = !errors.Any();
            Errors = errors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Instantiates new successful validation result
        /// </summary>
        public static ValidationResult Success => new ValidationResult();

        /// <summary>
        /// Instantiates new failed validation result
        /// </summary>
        /// <param name="errors">Validation errors</param>
        /// <returns></returns>
        public static ValidationResult Error(IEnumerable<ValidationError> errors) => new ValidationResult(errors);

        public bool IsValid { get; }

        public IReadOnlyCollection<ValidationError> Errors { get; }
    }
}
