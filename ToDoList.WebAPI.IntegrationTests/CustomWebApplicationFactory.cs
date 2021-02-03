using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ToDoList.WebAPI.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.IntegrationTests.json");
                config.AddJsonFile(configPath);
            });
        }
    }
}
