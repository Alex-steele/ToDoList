using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Commands;
using ToDoList.Core.Models;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Tests.Commands
{
    [TestFixture]
    public class AddCommandTests
    {
        private IToDoListRepository repository;
        private IAddCommandValidator validator;
        private AddCommand sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IToDoListRepository>();
            validator = A.Fake<IAddCommandValidator>();

            sut = new AddCommand(repository, validator);
        }

        [Test]
        public void Execute_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.Execute(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Execute_ModelIsInvalid_ReturnsCorrectValidationError()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = "Invalid Value"
            };

            A.CallTo(() => validator.Validate(testModel))
                .Returns(ValidationResult.Error(new List<ValidationError>
                {
                    new ValidationError(nameof(testModel.ItemValue), "Test error")
                }));

            // Act
            var result = sut.Execute(testModel);

            // Assert
            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Value == testModel.ItemValue)))
                .MustNotHaveHappened();

            Assert.That(result.Result, Is.EqualTo(CommandResult.ValidationError));

            Assert.That(result.Validation.IsValid, Is.False);
            Assert.That(result.Validation.Errors.Single().PropertyName, Is.EqualTo(nameof(testModel.ItemValue)));
            Assert.That(result.Validation.Errors.Single().ErrorMessage, Is.EqualTo("Test error"));
        }

        [Test]
        public void Execute_ModelIsValid_CorrectItemIsAddedAndReturnsSuccess()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = "Valid Value"
            };

            A.CallTo(() => validator.Validate(testModel))
                .Returns(ValidationResult.Success);

            // Act
            var result = sut.Execute(testModel);

            // Assert
            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Value == testModel.ItemValue)))
                .MustHaveHappenedOnceExactly();

            Assert.That(result.Result, Is.EqualTo(CommandResult.Success));
        }
    }
}