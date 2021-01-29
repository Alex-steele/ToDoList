using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToDoList.Core.Commands;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Mappers;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Queries;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Data.Configuration;
using ToDoList.Data.QueryableProviders;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configuration => configuration.AddConsole());

            services.AddSingleton<IListItemMapper, ListItemMapper>();
            services.AddSingleton<IAddCommandValidator, AddCommandValidator>();

            services.AddTransient(typeof(IQueryableProvider<>), typeof(QueryableProvider<>));

            services.AddTransient<IAddCommand, AddCommand>();
            services.AddTransient<ICompleteCommand, CompleteCommand>();
            services.AddTransient<IGetListQuery, GetListQuery>();
            services.AddTransient<IDeleteCommand, DeleteCommand>();
            services.ConfigureDataServices(Configuration.GetConnectionString("ToDoListDB"));
            services.AddTransient<IReadOnlyRepository, EFReadOnlyRepository>();
            services.AddTransient<IWriteRepository, EFWriteRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
