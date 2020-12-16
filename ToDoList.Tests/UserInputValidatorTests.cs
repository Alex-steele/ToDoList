using NUnit.Framework;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Enums;
using ToDoList.Core.Validators.Interfaces;

namespace ToDoList.Tests
{
    public class UserInputValidatorTests
    {
        private IUserInputValidator sut;

        [SetUp]
        public void SetUp()
        {
            sut = new UserInputValidator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_InputIsNullOrWhiteSpace_ReturnsInvalid(string input)
        {
            // Act
            var result = sut.Validate(input);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Invalid));
        }
    }
}
