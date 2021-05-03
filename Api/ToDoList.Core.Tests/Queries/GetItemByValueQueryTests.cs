using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.Data.Wrappers;

namespace ToDoList.Core.Tests.Queries
{
    public class GetItemByValueQueryTests
    {
        private IGetItemByValueQueryValidator validator;
        private IReadOnlyRepository repository;
        private IListItemMapper mapper;
        private GetItemByValueQuery sut;

        [SetUp]
        public void SetUp()
        {
            validator = A.Fake<IGetItemByValueQueryValidator>();
            repository = A.Fake<IReadOnlyRepository>();
            mapper = A.Fake<IListItemMapper>();

            sut = new GetItemByValueQuery(validator, repository, mapper);
        }

        [Test]
        public void ExecuteAsync_ModelIsNull_ThrowsException()
        {
            Assert.That(() => sut.ExecuteAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task ExecuteAsync_ItemValueIsInvalid_ReturnsInvalid()
        {
            // Arrange
            var model = new GetItemByValueQueryModel
            {
                ItemValue = null
            };

            A.CallTo(() => validator.Validate(model))
                .Returns(ValidationResult.Error(new List<ValidationError>
                {
                    new ValidationError(nameof(model.ItemValue), "Test error")
                }));

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert

            Assert.That(result.Result, Is.EqualTo(QueryResult.ValidationError));
        }

        [Test]
        public async Task Execute_GetByValueReturnsError_ReturnsError()
        {
            // Arrange
            var model = new GetItemByValueQueryModel
            {
                ItemValue = "Test"
            };

            A.CallTo(() => validator.Validate(model)).Returns(ValidationResult.Success);

            A.CallTo(() => repository.GetByValueAsync(model.ItemValue))
                .Returns(RepoResultWrapper<IEnumerable<ListItem>>.Error());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.Error));
        }

        [Test]
        public async Task Execute_GetByValueReturnsNotFound_ReturnsNotFound()
        {
            // Arrange
            var model = new GetItemByValueQueryModel
            {
                ItemValue = "Test"
            };

            A.CallTo(() => validator.Validate(model)).Returns(ValidationResult.Success);

            A.CallTo(() => repository.GetByValueAsync(model.ItemValue))
                .Returns(RepoResultWrapper<IEnumerable<ListItem>>.NotFound());

            // Act
            var result = await sut.ExecuteAsync(model);

            // Assert
            Assert.That(result.Result, Is.EqualTo(QueryResult.NotFound));
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

            A.CallTo(() => validator.Validate(model)).Returns(ValidationResult.Success);

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
