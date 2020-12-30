using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Commands;
using ToDoList.Core.Models;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Tests
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
        public void Execute_GetByIdReturnsNull_ItemIsNotCompleted()
        {
            // Arrange
            var testModel = new CompleteCommandModel
            {
                ItemId = 1
            };

            A.CallTo(() => repository.GetById(testModel.ItemId)).Returns(null);

            // Act
            sut.Execute(testModel);

            // Assert
            A.CallTo(() => repository.Complete(A<ListItem>.That.Matches(x => x.Id == 1)))
                .MustNotHaveHappened();
        }

        [Test]
        public void Execute_GetByIdReturnsItem_ItemIsCompleted()
        {
            // Arrange
            var testModel = new CompleteCommandModel
            {
                ItemId = 1
            };

            A.CallTo(() => repository.GetById(testModel.ItemId)).Returns(new ListItem
            {
                Id = 1,
                Completed = false
            });

            // Act
            sut.Execute(testModel);

            // Assert
            A.CallTo(() => repository.Complete(A<ListItem>.That.Matches(x => x.Id == 1)))
                .MustHaveHappenedOnceExactly();
        }
    }
}
