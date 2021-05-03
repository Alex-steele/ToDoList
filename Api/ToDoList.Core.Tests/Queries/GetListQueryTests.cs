using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Core.Tests.Queries
{
    public class GetListQueryTests
    {
        private IReadOnlyRepository repository;
        private IListItemMapper mapper;
        private GetListQuery sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IReadOnlyRepository>();
            mapper = A.Fake<IListItemMapper>();

            sut = new GetListQuery(repository, mapper);
        }

        [Test]
        public async Task Execute_GetAllReturnsError_ReturnsError()
        {
            // Arrange
            A.CallTo(() => repository.GetAllAsync()).Returns(RepoResultWrapper<IEnumerable<ListItem>>.Error());

            // Act
            var result = await sut.ExecuteAsync();

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Error));
        }

        [Test]
        public async Task Execute_GetAllReturnsList_ReturnsSuccessAndPayload()
        {
            // Arrange
            var testList = new List<ListItem>
            {
                new ListItem("TestListItem")
            };

            var mappedItem = new ListItemModel
            {
                Value = "TestListItem"
            };

            A.CallTo(() => repository.GetAllAsync()).Returns(RepoResultWrapper<IEnumerable<ListItem>>.Success(testList));

            A.CallTo(() => mapper.Map(testList.Single())).Returns(mappedItem);

            // Act
            var result = await sut.ExecuteAsync();

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Success));

            Assert.That(result.Payload.Single(), Is.EqualTo(mappedItem));
        }
    }
}
