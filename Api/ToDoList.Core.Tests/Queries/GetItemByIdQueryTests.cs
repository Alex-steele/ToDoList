using FakeItEasy;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Core.Tests.Queries
{
    public class GetItemByIdQueryTests
    {
        private IReadOnlyRepository repository;
        private IListItemMapper mapper;
        private GetItemByIdQuery sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IReadOnlyRepository>();
            mapper = A.Fake<IListItemMapper>();

            sut = new GetItemByIdQuery(repository, mapper);
        }

        [Test]
        public void ExecuteAsync_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task Execute_GetByIdReturnsError_ReturnsError()
        {
            // Arrange
            var model = new GetItemByIdQueryModel
            {
                ItemId = 1
            };

            A.CallTo(() => repository.GetByIdAsync(model.ItemId)).Returns(RepoResultWrapper<ListItem>.Error());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Error));
        }

        [Test]
        public async Task Execute_GetByIdReturnsNotFound_ReturnsNotFound()
        {
            // Arrange
            var model = new GetItemByIdQueryModel
            {
                ItemId = 1
            };

            A.CallTo(() => repository.GetByIdAsync(model.ItemId)).Returns(RepoResultWrapper<ListItem>.NotFound());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.NotFound));
        }

        [Test]
        public async Task Execute_GetByIdReturnsList_ReturnsSuccessAndPayload()
        {
            // Arrange
            var model = new GetItemByIdQueryModel
            {
                ItemId = 1
            };

            var testItem = new ListItem
            {
                Id = 1
            };

            var mappedItem = new ListItemModel
            {
                Id = 1
            };

            A.CallTo(() => repository.GetByIdAsync(model.ItemId)).Returns(RepoResultWrapper<ListItem>.Success(testItem));

            A.CallTo(() => mapper.Map(testItem)).Returns(mappedItem);

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Success));

            Assert.That(result.Payload.Single(), Is.EqualTo(mappedItem));
        }
    }
}
