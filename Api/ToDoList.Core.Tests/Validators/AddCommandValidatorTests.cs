using System.Linq;
using NUnit.Framework;
using ToDoList.Core.Models;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;

namespace ToDoList.Core.Tests.Validators
{
    public class AddCommandValidatorTests
    {
        private IAddCommandValidator sut;

        [SetUp]
        public void SetUp()
        {
            sut = new AddCommandValidator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ItemValueIsNullOrWhiteSpace_ReturnsCorrectError(string itemValue)
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = itemValue
            };

            // Act
            var result = sut.Validate(testModel);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Single().PropertyName, Is.EqualTo(nameof(testModel.ItemValue)));
            Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("item value is required"));
        }

        [Test]
        public void Validate_ItemValueIsUnder200_ReturnsValid()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = new string('a', 199)
            };

            // Act
            var result = sut.Validate(testModel);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void Validate_ItemValueIs200_ReturnsValid()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = new string('a', 200)
            };

            // Act
            var result = sut.Validate(testModel);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void Validate_ItemValueIsOver200_ReturnsCorrectError()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = new string('a', 201)
            };

            // Act
            var result = sut.Validate(testModel);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Single().PropertyName, Is.EqualTo(nameof(testModel.ItemValue)));
            Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("item value must be 200 characters or less"));
        }
    }
}
