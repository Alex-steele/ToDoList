using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDoList.Core.Models;
using ToDoList.Core.Queries;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators;
using ToDoList.Data.Entities;
using ToDoList.WebAPI.Integration.Tests.Models;
using Xunit;

namespace ToDoList.WebAPI.Integration.Tests
{
    public class ToDoListControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;
        private readonly HttpClient httpClient;

        public ToDoListControllerTests(WebApplicationFactory<Startup> factory)
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

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetList_ReturnsExpectedResponse()
        {
            // Act
            var model = await httpClient.GetFromJsonAsync<List<ListItem>>("");

            // Assert
            Assert.NotNull(model);
            Assert.NotEmpty(model);

            Assert.NotNull(model.FirstOrDefault()?.Value);
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
        public async Task AddItem_AddValidItem_Returns201CreatedResponse()
        {
            // Act
            var response = await httpClient.PostAsJsonAsync("", new AddCommandModel {ItemValue = "test"});

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CompleteItem_NonExistingID_Returns404NotFound()
        {
            // Act
            var response = await httpClient.PatchAsync("99999", null);

            //  Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CompleteItem_ExistingID_ReturnsSuccess()
        {
            // Act
            var response = await httpClient.PatchAsync("2", null);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteItem_NonExistingID_Returns404NotFound()
        {
            // Act
            var response = await httpClient.DeleteAsync("99999");

            //  Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Fact]
        //public async Task DeleteItem_ExistingID_ReturnsSuccess()
        //{
        //    // Act
        //    var response = await httpClient.DeleteAsync("1070");

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //}

        [Fact]
        public async Task Error_AppThrowsError_Returns500InternalServerError()
        {
            // Arrange
            var fakeGetListQuery = A.Fake<IGetListQuery>();

            A.CallTo(() => fakeGetListQuery.ExecuteAsync())
                .Throws(new Exception());

            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddTransient(s => fakeGetListQuery);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
