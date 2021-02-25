using NUnit.Framework;
using ToDoList.Console.Arguments;
using ToDoList.Console.Mappers;

namespace ToDoList.Console.Tests.Mappers
{
    public class AddCommandArgumentMapperTests
    {
        [Test]
        public void Map_InputArgs_ReturnsModelWithCorrectProperties()
        {
            // Arrange
            var testArgs = new AddCommandArguments
            {
                ItemValue = "Test"
            };

            // Act
            var mapper = new AddCommandArgumentMapper();
            var result = mapper.Map(testArgs);

            // Assert
            Assert.That(result.ItemValue, Is.EqualTo(testArgs.ItemValue));
        }
    }
}