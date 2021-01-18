using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Commands;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Core.Tests.Commands
{
    [TestFixture]
    public class CompleteCommandTests
    {
        private IWriteRepository writeRepository;
        private IReadOnlyRepository readRepository;
        private CompleteCommand sut;

        [SetUp]
        public void SetUp()
        {
            writeRepository = A.Fake<IWriteRepository>();
            readRepository = A.Fake<IReadOnlyRepository>();
            sut = new CompleteCommand(writeRepository, readRepository);
        }

        [Test]
        public void Execute_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task Execute_GetByIdReturnsNotFound_ReturnsNotFound()
        {
            // Arrange
            var testModel = new CompleteCommandModel
            {
                ItemId = 1
            };

            A.CallTo(() => readRepository.GetByIdForEditAsync(testModel.ItemId))
                .Returns(RepoResultWrapper<ListItem>.NotFound());

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Update(A<ListItem>.That.Matches(x => x.Id == 1)))
                .MustNotHaveHappened();

            A.CallTo(() => writeRepository.SaveChangesAsync()).MustNotHaveHappened();

            Assert.That(result.Result, Is.EqualTo(CommandResult.NotFound));
        }

        [Test]
        public async Task Execute_GetByIdReturnsItem_ItemIsCompletedAndReturnsSuccess()
        {
            // Arrange
            var testModel = new CompleteCommandModel
            {
                ItemId = 1
            };

            var testListItem = new ListItem("Test");

            A.CallTo(() => readRepository.GetByIdForEditAsync(testModel.ItemId))
                .Returns(RepoResultWrapper<ListItem>.Success(testListItem));

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Update(A<ListItem>.That.Matches(x => x.Value == "Test" && x.Completed)))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => writeRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            Assert.That(result.Result, Is.EqualTo(CommandResult.Success));
        }
    }
}
