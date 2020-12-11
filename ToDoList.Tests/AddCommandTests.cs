using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Commands;
using ToDoList.Data.Models;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Tests
{
    [TestFixture]
    public class AddCommandTests
    {
        private IToDoListRepository mockRepo;
        private AddCommand sut;

        [SetUp]
        public void SetUp()
        {
            mockRepo = A.Fake<IToDoListRepository>();

            sut = new AddCommand(mockRepo);
        }

        [Test]
        public void Execute_InputIsValid_CorrectItemAdded()
        {
            // Arrange
            var testItemValue = "Expected Value";

            // Act
            sut.Execute(testItemValue);

            // Assert
            A.CallTo(() =>
                    mockRepo.Add(A<ToDoListItem>.That.Matches(item =>
                        item.Value == "Expected Value" && item.Completed == false)))
                .MustHaveHappened();
        }
    }
}
