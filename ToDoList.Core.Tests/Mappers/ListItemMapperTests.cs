using NUnit.Framework;
using ToDoList.Core.Mappers;
using ToDoList.Data.Entities;

namespace ToDoList.Core.Tests.Mappers
{
    public class ListItemMapperTests
    {
        [Test]
        public void Map_InputIsListItem_ReturnsModelWithCorrectProperties()
        {
            // Arrange
            var testListItem = new ListItem("Test Value");

            // Act
            var mapper = new ListItemMapper();
            var result = mapper.Map(testListItem);

            // Assert
            Assert.That(result.Value, Is.EqualTo(testListItem.Value));
            Assert.That(result.Completed, Is.EqualTo(testListItem.Completed));
        }
    }
}
