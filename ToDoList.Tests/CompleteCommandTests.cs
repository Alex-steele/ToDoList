using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core;
using ToDoList.Core.Commands;

namespace ToDoList.Tests
{
    [TestFixture]
    public class CompleteCommandTests
    {
        [Test]
        public void CompleteItem_InputIdMatchesItemId_ItemIsCompleted()
        {
            // Arrange
            var listItems = new List<ListItem>
            {
                new ListItem
                {
                    Id = 1,
                    Completed = false
                }
            };

            const int id = 1;

            // Act
            CompleteCommand.CompleteItem(listItems, id);

            // Assert
            Assert.IsTrue(listItems.Single().Completed);
        }

        [Test]
        public void CompleteItem_InputIdDoesNotMatchItemId_ThrowsException()
        {
            // Arrange
            var listItems = new List<ListItem>
            {
                new ListItem
                {
                    Id = 1,
                    Completed = false
                }
            };

            const int id = 2;

            // Act & Assert
            Assert.That(() => CompleteCommand.CompleteItem(listItems, id), Throws.Exception);
        }

        [Test]
        public void CompleteItem_ListItemsIsNull_ThrowsException()
        {
            // Arrange
            var listItems = (List<ListItem>) null;
            const int id = 1;

            // Act & Assert
            Assert.That(() => CompleteCommand.CompleteItem(listItems, id), Throws.Exception);
        }

        [Test]
        public void CompleteItem_ListItemsIsEmpty_ThrowsException()
        {
            // Arrange
            var listItems = new List<ListItem>();
            const int id = 1;

            // Act & Assert
            Assert.That(() => CompleteCommand.CompleteItem(listItems, id), Throws.Exception);
        }
    }
}
