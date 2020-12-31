using NUnit.Framework;
using ToDoList.Core.Mappers;
using ToDoList.Data.Entities;

namespace ToDoList.Tests
{
    public class ListItemMapperTests
    {
        [Test]
        public void Map_InputIsListItem_ReturnsModelWithCorrectProperties()
        {
            // Arrange
            var testListItem = new ListItem
            {
                Id = 1,
                Value = "Test Value",
                Completed = false
            };

            // Act
            var mapper = new ListItemMapper();
            var result = mapper.Map(testListItem);

            // Assert
            Assert.That(result.Id, Is.EqualTo(testListItem.Id));
            Assert.That(result.Value, Is.EqualTo(testListItem.Value));
            Assert.That(result.Completed, Is.EqualTo(testListItem.Completed));
        }
    }
}
