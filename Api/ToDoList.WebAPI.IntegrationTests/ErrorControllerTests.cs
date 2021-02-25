using FakeItEasy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToDoList.Core.Queries.Interfaces;
using Xunit;

namespace ToDoList.WebAPI.IntegrationTests
{
    public class ErrorControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public ErrorControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/ToDoList/");
            var httpClient = factory.CreateClient();
        }

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

            client.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse("Basic dGVzdHVzZXJAdGVzdC5jb206dGVzdA==");

            // Act
            var response = await client.GetAsync("");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
