using FakeItEasy;
using NUnit.Framework;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.Core.Commands;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Core.Tests.Commands
{
    [TestFixture]
    public class DeleteCommandTests
    {
        private IWriteRepository writeRepository;
        private IReadOnlyRepository readRepository;
        private DeleteCommand sut;

        [SetUp]
        public void SetUp()
        {
            writeRepository = A.Fake<IWriteRepository>();
            readRepository = A.Fake<IReadOnlyRepository>();
            sut = new DeleteCommand(writeRepository, readRepository);
        }

        [Test]
        public void ExecuteAsync_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task ExecuteAsync_GetByIdReturnsNotFound_ReturnsNotFound()
        {
            // Arrange
            var testModel = new DeleteCommandModel
            {
                ItemId = 1
            };

            A.CallTo(() => readRepository.GetByIdForEditAsync(testModel.ItemId))
                .Returns(RepoResultWrapper<ListItem>.NotFound());

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Delete(A<ListItem>.That.Matches(x => x.Id == 1)))
                .MustNotHaveHappened();

            A.CallTo(() => writeRepository.SaveChangesAsync()).MustNotHaveHappened();

            Assert.That(result.Result, Is.EqualTo(CommandResult.NotFound));
        }

        [Test]
        public async Task ExecuteAsync_GetByIdReturnsItem_ItemIsDeletedAndCallsSaveChanges()
        {
            // Arrange
            var testModel = new DeleteCommandModel
            {
                ItemId = 1
            };

            var testListItem = new ListItem("Test");

            A.CallTo(() => readRepository.GetByIdForEditAsync(testModel.ItemId))
                .Returns(RepoResultWrapper<ListItem>.Success(testListItem));

            A.CallTo(() => writeRepository.SaveChangesAsync()).Returns(RepoResultWrapper<Unit>.Success(Unit.Default));

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Delete(A<ListItem>.That.Matches(x => x.Value == "Test")))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => writeRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task ExecuteAsync_SaveChangesReturnsError_ReturnsError()
        {
            // Arrange
            var testModel = new DeleteCommandModel
            {
                ItemId = 1
            };

            var testListItem = new ListItem("Test");

            A.CallTo(() => readRepository.GetByIdForEditAsync(testModel.ItemId))
                .Returns(RepoResultWrapper<ListItem>.Success(testListItem));

            A.CallTo(() => writeRepository.SaveChangesAsync()).Returns(RepoResultWrapper<Unit>.Error());

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            Assert.That(result.Result, Is.EqualTo(CommandResult.Error));
        }

        [Test]
        public async Task ExecuteAsync_SaveChangesReturnsSuccess_ReturnsSuccess()
        {
            // Arrange
            var testModel = new DeleteCommandModel
            {
                ItemId = 1
            };

            var testListItem = new ListItem("Test");

            A.CallTo(() => readRepository.GetByIdForEditAsync(testModel.ItemId))
                .Returns(RepoResultWrapper<ListItem>.Success(testListItem));

            A.CallTo(() => writeRepository.SaveChangesAsync()).Returns(RepoResultWrapper<Unit>.Success(Unit.Default));

            // Act
            var result = await sut.ExecuteAsync(testModel);

            // Assert
            A.CallTo(() => writeRepository.Delete(A<ListItem>.That.Matches(x => x.Value == "Test")))
                .MustHaveHappenedOnceExactly();

            Assert.That(result.Result, Is.EqualTo(CommandResult.Success));
        }
    }
}
