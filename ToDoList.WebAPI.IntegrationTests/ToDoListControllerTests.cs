using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.WebAPI.Integration.Tests.Models;
using Xunit;

namespace ToDoList.WebAPI.IntegrationTests
{
    public class ToDoListControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly HttpClient httpClient;

        public ToDoListControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/ToDoList/");
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetList_ReturnsSuccess()
        {
            // Act
            var response = await httpClient.GetAsync("");
            var listItems = await httpClient.GetFromJsonAsync<List<ListItem>>("");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetList_ReturnsExpectedResponse()
        {
            // Arrange
            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ToDoListContext>();
                context.ListItems.Add(new ListItem("GetListTest"));
                await context.SaveChangesAsync();
            }

            // Act
            var listItems = await httpClient.GetFromJsonAsync<List<ListItem>>("");

            // Assert
            Assert.Equal("GetListTest", listItems?.Last().Value);
        }

        [Fact]
        public async Task AddItem_AddInvalidItem_Returns400BadRequestWithCorrectErrors()
        {
            // Act
            var response = await httpClient.PostAsJsonAsync("", new AddCommandModel { ItemValue = "" });

            var validationResult = await response.Content.ReadFromJsonAsync<TestValidationResult>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("ItemValue", validationResult?.Errors.Single().PropertyName);
            Assert.Equal("item value is required", validationResult?.Errors.Single().ErrorMessage);
        }

        [Fact]
        public async Task AddItem_AddValidItem_AddsCorrectItemAndReturns201Created()
        {
            // Act
            var response = await httpClient.PostAsJsonAsync("", new AddCommandModel { ItemValue = "AddItemTest" });

            string testItemValue;
            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ToDoListContext>();
                testItemValue = context.ListItems.OrderBy(x => x.Id).Last().Value;
            }

            // Assert
            Assert.Equal("AddItemTest", testItemValue);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CompleteItem_NonExistingID_Returns404NotFound()
        {
            // Act
            var response = await httpClient.PatchAsync(int.MaxValue.ToString(), null);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CompleteItem_ExistingID_ReturnsSuccess()
        {
            // Arrange
            int itemId;

            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ToDoListContext>();
                context.ListItems.Add(new ListItem("CompleteItemTest"));
                await context.SaveChangesAsync();
                itemId = context.ListItems.OrderBy(x => x.Id).Last().Id;
            }

            // Act
            var response = await httpClient.PatchAsync(itemId.ToString(), null);

            ListItem completedItem;
            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ToDoListContext>();
                completedItem = context.ListItems.Single(x => x.Id == itemId);
            }

            // Assert
            Assert.True(completedItem.Completed);
            Assert.Equal("CompleteItemTest", completedItem.Value);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteItem_NonExistingID_Returns404NotFound()
        {
            // Act
            var response = await httpClient.DeleteAsync(int.MaxValue.ToString());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteItem_ExistingID_ReturnsSuccess()
        {
            // Arrange
            int itemId;

            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ToDoListContext>();
                context.ListItems.Add(new ListItem("DeleteItemTest"));
                await context.SaveChangesAsync();
                itemId = context.ListItems.OrderBy(x => x.Id).Last().Id;
            }

            // Act
            var response = await httpClient.DeleteAsync(itemId.ToString());

            ListItem deletedItem;
            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ToDoListContext>();
                deletedItem = context.ListItems.SingleOrDefault(x => x.Id == itemId);
            }

            // Assert
            Assert.Null(deletedItem);
            response.EnsureSuccessStatusCode();
        }
    }
}
