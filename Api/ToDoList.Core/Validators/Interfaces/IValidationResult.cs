using System.Collections.Generic;

namespace ToDoList.Core.Validators.Interfaces
{
    public interface IValidationResult
    {
        bool IsValid { get; }

        IReadOnlyCollection<ValidationError> Errors { get; }
    }
}
