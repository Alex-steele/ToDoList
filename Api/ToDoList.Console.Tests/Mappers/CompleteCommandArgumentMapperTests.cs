using NUnit.Framework;
using ToDoList.Console.Arguments;
using ToDoList.Console.Mappers;

namespace ToDoList.Console.Tests.Mappers
{
    public class CompleteCommandArgumentMapperTests
    {
        [Test]
        public void Map_InputArgs_ReturnsModelWithCorrectProperties()
        {
            // Arrange
            var testArgs = new CompleteCommandArguments
            {
                ItemId = 1
            };

            // Act
            var mapper = new CompleteCommandArgumentMapper();
            var result = mapper.Map(testArgs);

            // Assert
            Assert.That(result.ItemId, Is.EqualTo(testArgs.ItemId));
        }
    }
}
