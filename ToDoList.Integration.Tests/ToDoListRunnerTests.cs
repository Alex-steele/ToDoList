using FakeItEasy;
using NUnit.Framework;
using System.Linq;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Data;

namespace ToDoList.Integration.Tests
{
    public class ToDoListRunnerTests
    {
        private IToDoListServiceContainer serviceProvider;
        private IToDoListRunner runner;
        private ToDoListContext context;

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ToDoListServiceContainer();
            runner = serviceProvider.GetService<IToDoListRunner>();
            context = serviceProvider.GetService<ToDoListContext>();
        }

        [Test]
        public void AddValidItem_AddsItem()
        {
            // Arrange
            var listItems = context.ListItems.ToList();

            // Act
            runner.Run(serviceProvider, new[] { "add", "-i", "Test" });

            var updatedListItems = context.ListItems.ToList();

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count + 1));
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test"));
            Assert.That(updatedListItems.Last().Completed, Is.False);
        }

        [Test]
        public void AddInvalidItem_DoesNotAddItem()
        {
            // Arrange
            var listItems = context.ListItems.ToList();
            var resultHandler = A.Fake<IAddResultHandler>();

            // Act
            runner.Run(serviceProvider, new[] { "add", "-i", "" });

            var updatedListItems = context.ListItems.ToList();

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count));
        }

        [Test]
        public void CompleteItemValidId_CompletesCorrectItem()
        {
            // Arrange
            var listItems = context.ListItems.ToList();

            // Act
            runner.Run(serviceProvider, new[] { "add", "-i", "Test2" });

            var testItemId = context.ListItems.ToList().Last().Id;

            runner.Run(serviceProvider, new[] { "complete", "-d", $"{testItemId}" });

            var updatedListItems = context.ListItems.ToList();

            // Assert
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test2"));
            Assert.That(updatedListItems.Last().Completed, Is.True);
        }

        [Test]
        public void CompleteItemInvalidId_DoesNotCompleteAnyItem()
        {
            // Arrange
            var listItems = context.ListItems.ToList();

            // Act
            runner.Run(serviceProvider, new[] { "complete", "-d", "9999999999" });

            var updatedListItems = context.ListItems.ToList();

            // Assert
            for (var i = 0; i < listItems.Count; i++)
            {
                Assert.That(updatedListItems[i].Completed, Is.EqualTo(listItems[i].Completed));
            }
        }
    }
}
