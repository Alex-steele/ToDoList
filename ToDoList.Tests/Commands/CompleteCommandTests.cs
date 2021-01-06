using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Commands;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Tests.Commands
{
    [TestFixture]
    public class CompleteCommandTests
    {
        private IToDoListRepository repository;
        private CompleteCommand sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IToDoListRepository>();
            sut = new CompleteCommand(repository);
        }

        [Test]
        public void Execute_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.Execute(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Execute_GetByIdReturnsNull_ReturnsNotFound()
        {
            // Arrange
            var testModel = new CompleteCommandModel
            {
                ItemId = 1
            };

            A.CallTo(() => repository.GetById(testModel.ItemId)).Returns(null);

            // Act
            var result = sut.Execute(testModel);

            // Assert
            A.CallTo(() => repository.Complete(A<ListItem>.That.Matches(x => x.Id == 1)))
                .MustNotHaveHappened();

            Assert.That(result.Result, Is.EqualTo(CommandResult.NotFound));
        }

        [Test]
        public void Execute_GetByIdReturnsItem_ItemIsCompletedAndReturnsSuccess()
        {
            // Arrange
            var testModel = new CompleteCommandModel
            {
                ItemId = 1
            };

            A.CallTo(() => repository.GetById(testModel.ItemId)).Returns(new ListItem("Test"));

            // Act
            var result = sut.Execute(testModel);

            // Assert
            A.CallTo(() => repository.Complete(A<ListItem>.That.Matches(x => x.Value == "Test")))
                .MustHaveHappenedOnceExactly();

            Assert.That(result.Result, Is.EqualTo(CommandResult.Success));
        }
    }
}
