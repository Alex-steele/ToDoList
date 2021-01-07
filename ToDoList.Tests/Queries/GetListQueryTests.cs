using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Tests.Queries
{
    public class GetListQueryTests
    {
        private IToDoListRepository repository;
        private IListItemMapper mapper;
        private GetListQuery sut;

        [SetUp]
        public void SetUp()
        {
            repository = A.Fake<IToDoListRepository>();
            mapper = A.Fake<IListItemMapper>();

            sut = new GetListQuery(repository, mapper);
        }

        [Test]
        public void Execute_GetAllReturnsNull_ReturnsError()
        {
            // Arrange
            A.CallTo(() => repository.GetAll()).Returns(null);

            // Act
            var result = sut.Execute();

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Error));
        }

        [Test]
        public void Execute_GetAllReturnsList_ReturnsSuccessAndPayload()
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

            A.CallTo(() => repository.GetAll()).Returns(testList);

            A.CallTo(() => mapper.Map(testList.Single())).Returns(mappedItem);

            // Act
            var result = sut.Execute();

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Success));

            Assert.That(result.Payload.Single(), Is.EqualTo(mappedItem));
        }
    }
}
