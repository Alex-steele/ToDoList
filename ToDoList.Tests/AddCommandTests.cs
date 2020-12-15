using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core;
using ToDoList.Core.Commands;

namespace ToDoList.Tests
{
    [TestFixture]
    public class AddCommandTests
    {
        [Test]
        public void AddItem_ListItemsIsNull_ThrowsException()
        {
            // Arrange
            var listItems = (List<ListItem>) null;
            const string itemValue = "Test Value";

            // Act & Assert
            Assert.That(() => AddCommand.AddItem(listItems, itemValue), Throws.Exception);
        }

        [Test]
        public void AddItem_ListItemsIsEmpty_AddsItemWithId1()
        {
            // Arrange
            var listItems = new List<ListItem>();
            const string itemValue = "Test Value";

            // Act
            AddCommand.AddItem(listItems, itemValue);

            // Assert
            Assert.That(listItems.Single().Id, Is.EqualTo(1));
            Assert.That(listItems.Single().Value, Is.EqualTo("Test Value"));
        }

        [Test]
        public void AddItem_ListItemsIsNotEmpty_AddsItemWithNextId()
        {
            // Arrange
            var listItems = new List<ListItem>
            {
                new ListItem
                {
                    Id = 1
                }
            };

            const string itemValue = "Test Value";

            // Act
            AddCommand.AddItem(listItems, itemValue);

            // Assert
            Assert.That(listItems.Last().Id, Is.EqualTo(2));
            Assert.That(listItems.Last().Value, Is.EqualTo("Test Value"));
        }
    }
}