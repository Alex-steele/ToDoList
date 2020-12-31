using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ToDoList.Core.Validators;

namespace ToDoList.Tests.Validators
{
    public class ValidationResultTests
    {
        [Test]
        public void ParameterlessConstructorUsed_CreatesValidValidationResult()
        {
            var result = new ValidationResult();

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }

        [Test]
        public void ErrorsIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => new ValidationResult(null), Throws.ArgumentNullException);
        }

        [Test]
        public void ErrorsIsEmpty_CreatesValidValidationResult()
        {
            var result = new ValidationResult(new List<ValidationError>());

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }

        [Test]
        public void ErrorsIsPopulated_CreatesInValidValidationResultWithErrors()
        {
            var result = new ValidationResult(new List<ValidationError>
            {
                new ValidationError("Test property name", "Test error message")
            });

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Single().PropertyName, Is.EqualTo("Test property name"));
            Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Test error message"));
        }
    }
}
