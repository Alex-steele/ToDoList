using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Validators.Interfaces;

namespace ToDoList.Core.Validators
{
    public class ValidationResult : IValidationResult
    {
        /// <summary>
        /// Instantiates new successful validation result
        /// </summary>
        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<ValidationError>();
        }

        /// <summary>
        /// Instantiates new failed validation result
        /// </summary>
        /// <param name="errors"></param>
        public ValidationResult(IEnumerable<ValidationError> errors)
        {
            Check.NotNull(errors, nameof(errors));

            IsValid = !errors.Any();
            Errors = errors.ToList().AsReadOnly();
        }

        public bool IsValid { get; }

        public IReadOnlyCollection<ValidationError> Errors { get; }
    }
}
