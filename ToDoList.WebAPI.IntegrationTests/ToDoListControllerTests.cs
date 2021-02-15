﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private readonly ToDoListContext context;
        private readonly IServiceScope scope;

        public ToDoListControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/ToDoList/");
            httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic dGVzdHVzZXJAdGVzdC5jb206dGVzdA==");

            scope = factory.Services.CreateScope();
            context = scope.ServiceProvider.GetService<ToDoListContext>();
        }

        [Fact]
        public async Task GetList_ReturnsSuccess()
        {
            // Act
            var response = await httpClient.GetAsync("");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetList_ReturnsExpectedResponse()
        {
            // Arrange
            context.ListItems.Add(new ListItem("GetListTest"));
            await context.SaveChangesAsync();

            // Act
            var listItems = await httpClient.GetFromJsonAsync<List<ListItem>>("");

            // Assert
            Assert.Equal("GetListTest", listItems?.Last().Value);

            scope.Dispose();
        }

        [Fact]
        public async Task GetItemByValueNotFuzzy_ItemValueInvalid_Returns400BadRequest()
        {
            // Arrange
            var invalidValue = string.Empty;

            // Act
            var response = await httpClient.GetAsync($"searchByValue?ItemValue={invalidValue}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetItemByValueNotFuzzy_ItemValueDoesNotMatchExistingItem_Returns404NotFound()
        {
            // Arrange
            var nonExistingItemValue = Guid.NewGuid().ToString();

            // Act
            var response = await httpClient.GetAsync($"searchByValue?ItemValue={nonExistingItemValue}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetItemByValueNotFuzzy_ItemValueMatchesExistingItemExactly_ReturnsCorrectListOfItems()
        {
            // Arrange
            context.ListItems.Add(new ListItem("GetItemByValueTest"));
            context.ListItems.Add(new ListItem("GetItemByValueTestNotFuzzy"));
            await context.SaveChangesAsync();


            // Act
            var listItems = await httpClient.GetFromJsonAsync<List<ListItem>>("searchByValue?ItemValue=GetItemByValueTest");

            // Assert
            foreach (var item in listItems)
            {
                Assert.Equal("GetItemByValueTest", item.Value);
            }

            scope.Dispose();
        }

        [Fact]
        public async Task GetItemByValueFuzzy_ItemValueInvalid_Returns400BadRequest()
        {
            // Arrange
            var invalidValue = string.Empty;

            // Act
            var response = await httpClient.GetAsync($"searchByValue?ItemValue={invalidValue}&fuzzy=true");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetItemByValueFuzzy_ItemValueNotIncludedInExistingItem_Returns404NotFound()
        {
            // Arrange
            var nonExistingItemValue = Guid.NewGuid().ToString();

            // Act
            var response = await httpClient.GetAsync($"searchByValue?ItemValue={nonExistingItemValue}&fuzzy=true");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetItemByValueFuzzy_ItemValueIncludedInExistingItem_ReturnsCorrectListOfItems()
        {
            // Arrange
            context.ListItems.Add(new ListItem("GetItemByValueFuzzyTest1"));
            await context.SaveChangesAsync();

            // Act
            var listItems = await httpClient.GetFromJsonAsync<List<ListItem>>("searchByValue?ItemValue=GetItemByValueFuzzyTest&fuzzy=true");

            var fuzzyItems = listItems.Select(x => x.Value.Contains("GetItemByValueFuzzyTest") && x.Value != "GetItemByValueFuzzyTest");

            // Assert
            Assert.NotEmpty(fuzzyItems);

            foreach (var item in listItems)
            {
                Assert.Contains("GetItemByValueFuzzyTest", item.Value);
            }

            scope.Dispose();
        }

        [Fact]
        public async Task GetItemByDate_InvalidDate_Returns400BadRequest()
        {
            // Act
            var response = await httpClient.GetAsync("searchByDate?Date=2020-13-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetItemByDate_DateDoesNotMatchItems_ReturnsEmptyList()
        {
            // Act
            var response = await httpClient.GetAsync("searchByDate/Date=9999-1-1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetItemByDate_DateMatchesItemsToNearestDay_ReturnsCorrectItems()
        {
            // Arrange
            context.ListItems.Add(new ListItem
            {
                Value = "GetItemByDateTest",
                Date = DateTime.MaxValue,
                Completed = false
            });
            await context.SaveChangesAsync();

            var expectedDate = DateTime.MaxValue.Date;

            // Act
            var listItems = await httpClient.GetFromJsonAsync<List<ListItem>>("searchByDate?Date=9999-12-31");

            // Assert
            foreach (var item in listItems)
            {
                Assert.Equal(item.Date, expectedDate, TimeSpan.FromDays(1));
            }
            scope.Dispose();
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
        public async Task AddItem_AddValidItem_AddsItemAndReturns201CreatedWithCorrectPayload()
        {
            // Act
            var response = await httpClient.PostAsJsonAsync("", new AddCommandModel { ItemValue = "AddItemTest" });

            var payload = await response.Content.ReadFromJsonAsync<ListItem>();

            var addedItem = context.ListItems.OrderBy(x => x.Id).Last();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("AddItemTest", addedItem.Value);
            Assert.Equal(payload.Value, addedItem.Value);
            Assert.Equal(payload.Id, addedItem.Id);

            scope.Dispose();
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
            context.ListItems.Add(new ListItem("CompleteItemTest"));
            await context.SaveChangesAsync();

            var itemId = context.ListItems.OrderBy(x => x.Id).Last().Id;

            scope.Dispose();

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
            context.ListItems.Add(new ListItem("DeleteItemTest"));
            await context.SaveChangesAsync();

            var itemId = context.ListItems.OrderBy(x => x.Id).Last().Id;

            // Act
            var response = await httpClient.DeleteAsync(itemId.ToString());
            var deletedItem = context.ListItems.SingleOrDefault(x => x.Id == itemId);

            // Assert
            Assert.Null(deletedItem);
            response.EnsureSuccessStatusCode();

            scope.Dispose();
        }
    }
}
