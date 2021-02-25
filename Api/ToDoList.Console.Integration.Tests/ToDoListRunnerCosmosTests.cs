using FakeItEasy;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Data.Cosmos.Repositories.Interfaces;

namespace ToDoList.Console.Integration.Tests
{
    public class ToDoListRunnerCosmosTests
    {
        private IToDoListServiceContainer serviceProvider;
        private IToDoListRunner runner;
        private ICosmosRepository repository;

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ToDoListServiceContainer();
            runner = serviceProvider.GetService<IToDoListRunner>();
            repository = serviceProvider.GetService<ICosmosRepository>();
        }

        [Test]
        public async Task AddValidItem_AddsItem()
        {
            // Arrange
            var repoResult = await repository.GetAllAsync();
            var listItems = repoResult.Payload;

            // Act
            runner.Run(new[] { "add", "-i", "Test" });

            var updatedRepoResult = await repository.GetAllAsync();
            var updatedListItems = updatedRepoResult.Payload;

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count + 1));
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test"));
            Assert.That(updatedListItems.Last().Completed, Is.False);
        }

        [Test]
        public async Task AddInvalidItem_DoesNotAddItem()
        {
            // Arrange
            var repoResult = await repository.GetAllAsync();
            var listItems = repoResult.Payload;

            var resultHandler = A.Fake<IAddResultHandler>();

            // Act
            runner.Run(new[] { "add", "-i", "" });

            var updatedRepoResult = await repository.GetAllAsync();
            var updatedListItems = updatedRepoResult.Payload;

            // Assert
            Assert.That(updatedListItems.Count, Is.EqualTo(listItems.Count));
        }

        [Test]
        public async Task CompleteItemValidId_CompletesCorrectItem()
        {
            // Arrange
            var repoResult = await repository.GetAllAsync();
            var listItems = repoResult.Payload;

            // Act
            runner.Run(new[] { "add", "-i", "Test2" });

            var itemRepoResult = await repository.GetAllAsync();
            var testItemId = itemRepoResult.Payload.ToList().Last().IntId;

            runner.Run(new[] { "complete", "-d", $"{testItemId}" });

            var updatedRepoResult = await repository.GetAllAsync();
            var updatedListItems = updatedRepoResult.Payload;

            // Assert
            Assert.That(updatedListItems.Last().Value, Is.EqualTo("Test2"));
            Assert.That(updatedListItems.Last().Completed, Is.True);
        }

        [Test]
        public async Task CompleteItemInvalidId_DoesNotCompleteAnyItem()
        {
            // Arrange
            var repoResult = await repository.GetAllAsync();
            var listItems = repoResult.Payload;

            var completedItems = listItems.Where(x => x.Completed);

            // Act
            runner.Run(new[] { "complete", "-d", "9999999999" });

            var updatedRepoResult = await repository.GetAllAsync();
            var updatedListItems = updatedRepoResult.Payload;

            var updatedCompletedItems = updatedListItems.Where(x => x.Completed);

            // Assert
            Assert.That(completedItems.Count(), Is.EqualTo(updatedCompletedItems.Count()));
        }
    }
}
