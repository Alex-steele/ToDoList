using System;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
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
    public class GetItemByDateQueryTests
    {
        private IReadOnlyRepository repository;
        private IListItemMapper mapper;
        private GetItemsByDateQuery sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IReadOnlyRepository>();
            mapper = A.Fake<IListItemMapper>();

            sut = new GetItemsByDateQuery(repository, mapper);
        }

        [Test]
        public void ExecuteAsync_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task Execute_GetByValueReturnsError_ReturnsError()
        {
            // Arrange
            var model = new GetItemByDateQueryModel
            {
                Date = DateTime.MinValue
            };

            A.CallTo(() => repository.GetByDateAsync(model.Date)).Returns(RepoResultWrapper<IEnumerable<ListItem>>.Error());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Error));
        }

        [Test]
        public async Task Execute_GetByValueReturnsNotFound_ReturnsNotFound()
        {
            // Arrange
            var model = new GetItemByDateQueryModel
            {
                Date = DateTime.MinValue
            };

            A.CallTo(() => repository.GetByDateAsync(model.Date)).Returns(RepoResultWrapper<IEnumerable<ListItem>>.NotFound());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.NotFound));
        }

        [Test]
        public async Task Execute_GetByValueReturnsList_ReturnsSuccessAndPayload()
        {
            // Arrange
            var model = new GetItemByDateQueryModel
            {
                Date = DateTime.MinValue
            };

            var testList = new List<ListItem>
            {
                new ListItem
                {
                    Date = DateTime.MinValue
                }
            };

            var mappedItem = new ListItemModel
            {
                Date = DateTime.MinValue
            };

            A.CallTo(() => repository.GetByDateAsync(model.Date)).Returns(RepoResultWrapper<IEnumerable<ListItem>>.Success(testList));

            A.CallTo(() => mapper.Map(testList.Single())).Returns(mappedItem);

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Success));

            Assert.That(result.Payload.Single(), Is.EqualTo(mappedItem));
        }
    }
}
