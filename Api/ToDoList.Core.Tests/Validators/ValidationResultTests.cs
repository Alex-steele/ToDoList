using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ToDoList.Core.Validators;

namespace ToDoList.Core.Tests.Validators
{
    public class ValidationResultTests
    {
        [Test]
        public void Success_SuccessConstructorUsed_CreatesValidValidationResult()
        {
            var result = ValidationResult.Success;

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }

        [Test]
        public void Error_ErrorsIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => ValidationResult.Error(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Error_ErrorsIsEmpty_ThrowsArgumentNullException()
        {
            Assert.That(() => ValidationResult.Error(new List<ValidationError>()), Throws.ArgumentNullException);
        }

        [Test]
        public void Error_ErrorsIsPopulated_CreatesInValidValidationResultWithErrors()
        {
            var result = ValidationResult.Error(new List<ValidationError>
            {
                new ValidationError("Test property name", "Test error message")
            });

            Assert.That(result.IsValid, Is.False);

            Assert.That(result.Errors.Single().PropertyName, Is.EqualTo("Test property name"));
            Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Test error message"));
        }
    }
}
