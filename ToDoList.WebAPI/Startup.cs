using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ToDoList.Core.Commands;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Mappers;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Queries;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Configuration;
using ToDoList.Data.QueryableProviders;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Interfaces;
using ToDoList.WebAPI.Extensions;
using ToDoList.WebAPI.Logging;
using ToDoList.WebAPI.Middleware;
using ToDoList.WebAPI.Resolvers;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configuration => configuration.AddConsole());

            services.AddSingleton<IListItemMapper, ListItemMapper>();
            services.AddSingleton<IAddCommandValidator, AddCommandValidator>();
            services.AddSingleton<IGetItemByValueQueryValidator, GetItemByValueQueryValidator>();
            services.AddSingleton<IResultResolver<CommandResultWrapper>, CommandResultResolver>();
            services.AddSingleton<IResultResolver<QueryResultWrapper>, QueryResultResolver>();

            services.AddScoped(typeof(IQueryableProvider<>), typeof(QueryableProvider<>));
            services.AddScoped<IAddCommand, AddCommand>();
            services.AddScoped<ICompleteCommand, CompleteCommand>();
            services.AddScoped<IGetListQuery, GetListQuery>();
            services.AddScoped<IGetItemByValueQuery, GetItemByValueQuery>();
            services.AddScoped<IGetItemByValueFuzzyQuery, GetItemByValueFuzzyQuery>();
            services.AddScoped<IGetItemByDateQuery, GetItemByDateQuery>();
            services.AddScoped<IDeleteCommand, DeleteCommand>();
            services.ConfigureDataServices(Configuration.GetConnectionString("ToDoListDB"));
            services.AddScoped<IReadOnlyRepository, EFReadOnlyRepository>();
            services.AddScoped<IWriteRepository, EFWriteRepository>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/error");
            ////Custom error handling:
            //app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);

            app.UseRouting();

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
