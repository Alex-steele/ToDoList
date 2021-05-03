using NUnit.Framework;
using ToDoList.Core.Validators;

namespace ToDoList.Core.Tests.Validators
{
    public class ValidationErrorTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void PropertyNameIsNullOrWhiteSpace_ThrowsArgumentNullException(string propertyName)
        {
            Assert.That(() => new ValidationError(propertyName, "Test error message"), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ErrorMessageIsNullOrWhiteSpace_ThrowsArgumentNullException(string errorMessage)
        {
            Assert.That(() => new ValidationError("Test property name", errorMessage), Throws.ArgumentNullException);
        }

        [Test]
        public void PropertyNameAndErrorMessageArePopulated_DoesNotThrowException()
        {
            Assert.That(() => new ValidationError("Test property name", "Test error message"), Throws.Nothing);
        }
    }
}
