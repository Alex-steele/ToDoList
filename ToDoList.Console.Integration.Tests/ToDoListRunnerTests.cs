using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Data.Entities;
using ToDoList.Data.QueryableProviders;

namespace ToDoList.Console.Integration.Tests
{
    public class ToDoListRunnerTests
    {
        private IToDoListServiceContainer serviceProvider;
        private IToDoListRunner runner;
        private IQueryableProvider<ListItem> provider;

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ToDoListServiceContainer();
            runner = serviceProvider.GetService<IToDoListRunner>();
            provider = serviceProvider.GetService<IQueryableProvider<ListItem>>();
        }

        [Test]
        public async Task AddValidItem_AddsItem()
        {
            // Arrange
            var listItems = await provider.Set.ToListAsync();

            // Act
            runner.Run(new[] { "add", "-i", "Test" });

            var updatedListItems = await provider.Set.ToListAsync();

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count + 1));
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test"));
            Assert.That(updatedListItems.Last().Completed, Is.False);
        }

        [Test]
        public async Task AddInvalidItem_DoesNotAddItem()
        {
            // Arrange
            var listItems = await provider.Set.ToListAsync();
            var resultHandler = A.Fake<IAddResultHandler>();

            // Act
            runner.Run(new[] { "add", "-i", "" });

            var updatedListItems = await provider.Set.ToListAsync();

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count));
        }

        [Test]
        public async Task CompleteItemValidId_CompletesCorrectItem()
        {
            // Arrange
            var listItems = await provider.Set.ToListAsync();

            // Act
            runner.Run(new[] { "add", "-i", "Test2" });

            var itemRepoResult = await provider.Set.OrderBy(x => x.Id).LastAsync();
            var testItemId = itemRepoResult.Id;


            runner.Run(new[] { "complete", "-d", $"{testItemId}" });

            var updatedListItems = await provider.Set.ToListAsync();

            // Assert
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test2"));
            Assert.That(updatedListItems.Last().Completed, Is.True);
        }

        [Test]
        public async Task CompleteItemInvalidId_DoesNotCompleteAnyItem()
        {
            // Arrange
            var listItems = await provider.Set.ToListAsync();
            var completedItems = listItems.Where(x => x.Completed);

            // Act
            runner.Run(new[] { "complete", "-d", "9999999999" });

            var updatedListItems = await provider.Set.ToListAsync();
            var updatedCompletedItems = updatedListItems.Where(x => x.Completed);

            // Assert
            Assert.That(completedItems.Count(), Is.EqualTo(updatedCompletedItems.Count()));
        }
    }
}
