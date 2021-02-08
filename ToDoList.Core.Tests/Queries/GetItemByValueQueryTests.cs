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
    public class GetItemByValueQueryTests
    {
        private IReadOnlyRepository repository;
        private IListItemMapper mapper;
        private GetItemByValueQuery sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IReadOnlyRepository>();
            mapper = A.Fake<IListItemMapper>();

            sut = new GetItemByValueQuery(repository, mapper);
        }

        [Test]
        public void ExecuteAsync_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task ExecuteAsync_ItemValueIsNull_ReturnsSuccess()
        {
            // Arrange
            var model = new GetItemByValueQueryModel
            {
                ItemValue = null
            };

            A.CallTo(() => repository.GetByValueAsync(string.Empty))
                .Returns(RepoResultWrapper<IEnumerable<ListItem>>.Success(new List<ListItem>()));

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert

            Assert.That(result.Result, Is.EqualTo(QueryResult.Success));
        }

        [Test]
        public async Task Execute_GetByValueReturnsError_ReturnsError()
        {
            // Arrange
            var model = new GetItemByValueQueryModel
            {
                ItemValue = "Test"
            };

            A.CallTo(() => repository.GetByValueAsync(model.ItemValue))
                .Returns(RepoResultWrapper<IEnumerable<ListItem>>.Error());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Error));
        }

        [Test]
        public async Task Execute_GetByValueReturnsList_ReturnsSuccessAndPayload()
        {
            // Arrange
            var model = new GetItemByValueQueryModel
            {
                ItemValue = "TestListItem"
            };

            var testList = new List<ListItem>
            {
                new ListItem("TestListItem")
            };

            var mappedItem = new ListItemModel
            {
                Value = "TestListItem"
            };

            A.CallTo(() => repository.GetByValueAsync(model.ItemValue))
                .Returns(RepoResultWrapper<IEnumerable<ListItem>>.Success(testList));

            A.CallTo(() => mapper.Map(testList.Single())).Returns(mappedItem);

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Success));

            Assert.That(result.Payload.Single(), Is.EqualTo(mappedItem));
        }
    }
}
