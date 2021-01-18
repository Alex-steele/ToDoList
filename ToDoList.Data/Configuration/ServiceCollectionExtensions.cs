using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Data.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ToDoListContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
