using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Core.Commands;
using ToDoList.Core.Models;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.QueryableProviders;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Core.Tests.Commands
{
    [TestFixture]
    public class AddCommandTests
    {
        private IWriteRepository writeRepository;
        private IAddCommandValidator validator;
        private AddCommand sut;

        [SetUp]
        public void SetUp()
        {
            writeRepository = A.Fake<IWriteRepository>();
            validator = A.Fake<IAddCommandValidator>();

            sut = new AddCommand(writeRepository, validator);
        }

        [Test]
        public void ExecuteAsync_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task ExecuteAsync_ModelIsInvalid_ReturnsCorrectValidationError()
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
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Add(A<ListItem>.That.Matches(x => x.Value == testModel.ItemValue)))
                .MustNotHaveHappened();

            A.CallTo(() => writeRepository.SaveChangesAsync()).MustNotHaveHappened();

            Assert.That(result.Result, Is.EqualTo(CommandResult.ValidationError));

            Assert.That(result.Validation.IsValid, Is.False);
            Assert.That(result.Validation.Errors.Single().PropertyName, Is.EqualTo(nameof(testModel.ItemValue)));
            Assert.That(result.Validation.Errors.Single().ErrorMessage, Is.EqualTo("Test error"));
        }

        [Test]
        public async Task ExecuteAsync_ModelIsValid_CorrectItemIsAddedAndCallsSaveChanges()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = "Valid Value"
            };

            A.CallTo(() => validator.Validate(testModel))
                .Returns(ValidationResult.Success);

            A.CallTo(() => writeRepository.SaveChangesAsync()).Returns(RepoResultWrapper<Unit>.Success(Unit.Default));

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Add(A<ListItem>.That.Matches(x => x.Value == testModel.ItemValue)))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => writeRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task ExecuteAsync_SaveChangesReturnsError_ReturnsError()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = "Valid Value"
            };

            A.CallTo(() => validator.Validate(testModel))
                .Returns(ValidationResult.Success);

            A.CallTo(() => writeRepository.SaveChangesAsync()).Returns(RepoResultWrapper<Unit>.Error());

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            Assert.That(result.Result, Is.EqualTo(CommandResult.Error));
        }

        [Test]
        public async Task ExecuteAsync_SaveChangesReturnsSuccess_ReturnsCreatedWithItemPayload()
        {
            // Arrange
            var testModel = new AddCommandModel
            {
                ItemValue = "Valid Value"
            };

            A.CallTo(() => validator.Validate(testModel))
                .Returns(ValidationResult.Success);

            A.CallTo(() => writeRepository.SaveChangesAsync()).Returns(RepoResultWrapper<Unit>.Success(Unit.Default));

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            Assert.That(result.Result, Is.EqualTo(CommandResult.Created));
            Assert.That(result.Payload.Value, Is.EqualTo("Valid Value"));
        }
    }
}