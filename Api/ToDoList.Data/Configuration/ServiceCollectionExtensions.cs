using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Data.Cosmos;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Data.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services,
            string connectionString)
        {
            services.AddScoped<IReadOnlyRepository, EFReadOnlyRepository>();
            services.AddScoped<IWriteRepository, EFWriteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Config for Sql Server
            services.AddDbContext<ToDoListContext>(options => options.UseSqlServer(connectionString));

            //// Config for Cosmos EF
            //services.AddDbContext<ToDoListCosmosContext>(options => options.UseCosmos(
            //    connectionString,
            //    databaseName: "ToDoListCosmosDB"));

            //// Config for Cosmos
            //services.AddSingleton<ICosmosConfiguration>(_ => new CosmosConfiguration(connectionString));

            return services;
        }
    }
}
