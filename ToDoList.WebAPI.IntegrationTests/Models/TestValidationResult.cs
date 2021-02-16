using System.Collections.Generic;

namespace ToDoList.WebAPI.Integration.Tests.Models
{
    public class TestValidationResult
    {
        public bool IsValid { get; set; }

        public IEnumerable<TestValidationError> Errors { get; set; }
    }
}
