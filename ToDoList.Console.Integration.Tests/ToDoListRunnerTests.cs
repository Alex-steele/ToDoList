using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Data;

namespace ToDoList.Console.Integration.Tests
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
        public async Task AddValidItem_AddsItem()
        {
            // Arrange
            var listItems = await context.ListItems.ToListAsync();

            // Act
            runner.Run(new[] { "add", "-i", "Test" });

            var updatedListItems = await context.ListItems.ToListAsync();

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count + 1));
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test"));
            Assert.That(updatedListItems.Last().Completed, Is.False);
        }

        [Test]
        public async Task AddInvalidItem_DoesNotAddItem()
        {
            // Arrange
            var listItems = await context.ListItems.ToListAsync();
            var resultHandler = A.Fake<IAddResultHandler>();

            // Act
            runner.Run(new[] { "add", "-i", "" });

            var updatedListItems = await context.ListItems.ToListAsync();

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count));
        }

        [Test]
        public async Task CompleteItemValidId_CompletesCorrectItem()
        {
            // Arrange
            var listItems = await context.ListItems.ToListAsync();

            // Act
            runner.Run(new[] { "add", "-i", "Test2" });

            var testItemId = context.ListItems.ToList().Last().Id;

            runner.Run(new[] { "complete", "-d", $"{testItemId}" });

            var updatedListItems = await context.ListItems.ToListAsync();

            // Assert
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test2"));
            Assert.That(updatedListItems.Last().Completed, Is.True);
        }

        [Test]
        public async Task CompleteItemInvalidId_DoesNotCompleteAnyItem()
        {
            // Arrange
            var listItems = await context.ListItems.ToListAsync();

            // Act
            runner.Run(new[] { "complete", "-d", "9999999999" });

            var updatedListItems = await context.ListItems.ToListAsync();

            // Assert
            for (var i = 0; i < listItems.Count; i++)
            {
                Assert.That(updatedListItems[i].Completed, Is.EqualTo(listItems[i].Completed));
            }
        }
    }
}
