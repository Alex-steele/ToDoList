using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using ToDoList.Core;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;

namespace ToDoList.Tests
{
    public class ToDoListRunnerTests
    {
        private IAddCommand addCommand;
        private ICompleteCommand completeCommand;
        private IGetListQuery getListQuery;
        private ToDoListRunner sut;

        [SetUp]
        public void SetUp()
        {
            addCommand = A.Fake<IAddCommand>();
            completeCommand = A.Fake<ICompleteCommand>();
            getListQuery = A.Fake<IGetListQuery>();

            sut = new ToDoListRunner(addCommand, completeCommand, getListQuery);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Execute_InputIsNullOrWhiteSpace_ReturnsValidationError(string input)
        {
            // Act
            var result = sut.Execute(input);

            // Assert
            Assert.That(result.Result, Is.EqualTo(RunnerResult.ValidationError));
        }

        [Test]
        public void Execute_GetListReturnsNull_ReturnsInvalidOperation()
        {
            // Arrange
            const string input = "test input";

            A.CallTo(() => getListQuery.GetList())
                .Returns(null);

            // Act
            var result = sut.Execute(input);

            // Assert
            Assert.That(result.Result, Is.EqualTo(RunnerResult.InvalidOperation));
        }

        [Test]
        public void Execute_InputIsItemId_ReturnsSuccessAndCompletesItem()
        {
            // Arrange
            const string input = "1";

            A.CallTo(() => getListQuery.GetList())
                .Returns(new List<ListItem>
                {
                    new ListItem
                    {
                        Id = 1,
                        Completed = false
                    }
                });

            // Act
            var result = sut.Execute(input);

            // Assert
            A.CallTo(() => completeCommand.CompleteItem(1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => addCommand.AddItem("1")).MustNotHaveHappened();

            Assert.That(result.Result, Is.EqualTo(RunnerResult.Success));
        }

        [Test]
        public void Execute_InputIsNotItemId_ReturnsSuccessAndAddsItem()
        {
            // Arrange
            const string input = "test input";

            A.CallTo(() => getListQuery.GetList())
                .Returns(new List<ListItem>());

            // Act
            var result = sut.Execute(input);

            // Assert
            A.CallTo(() => addCommand.AddItem("test input")).MustHaveHappenedOnceExactly();

            Assert.That(result.Result, Is.EqualTo(RunnerResult.Success));
        }
    }
}
