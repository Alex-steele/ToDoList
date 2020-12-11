using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Commands;
using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Tests
{
    [TestFixture]
    public class CompleteCommandTests
    {
        private IToDoListRepository mockRepo;
        private CompleteCommand sut;

        [SetUp]
        public void SetUp()
        {
            mockRepo = A.Fake<IToDoListRepository>();

            sut = new CompleteCommand(mockRepo);
        }

        [Test]
        public void Execute_GetByIdReturnsItem_CorrectItemIsCompleted()
        {
            // Arrange
            const int testId = 1;

            A.CallTo(() => mockRepo.GetById(testId))
                .Returns(new ToDoListItem
                {
                    Id = testId
                });

            // Act
            sut.Execute(testId);

            // Assert
            A.CallTo(() =>
                    mockRepo.Complete(A<ToDoListItem>.That.Matches(item =>
                        item.Id == testId)))
                .MustHaveHappened();
        }
    }
}
