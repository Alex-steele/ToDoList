using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Data.Cosmos;

namespace ToDoList.Data.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services,
            string connectionString)
        {
            // Config for Sql Server
            services.AddDbContext<ToDoListContext>(options => options.UseSqlServer(connectionString));

            // Config for Cosmos
            services.AddDbContext<ToDoListCosmosContext>(options => options.UseCosmos(
                connectionString,
                databaseName: "ToDoListCosmosDB"));

            return services;
        }
    }
}
