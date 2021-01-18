using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Console.ResultHandlers;
using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Core.Commands;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Mappers;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Queries;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Data;
using ToDoList.Data.Configuration;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Console.Installers
{
    public class ToDoListServiceContainer : IToDoListServiceContainer
    {
        private readonly IServiceProvider serviceProvider;

        public ToDoListServiceContainer()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var services = new ServiceCollection();

            services.AddLogging(configuration => configuration.AddConsole()).AddTransient<ToDoListRunner>();
            services.AddTransient<IToDoListRunner, ToDoListRunner>();
            services.AddTransient<IAddCommandRunner, AddCommandRunner>();
            services.AddTransient<ICompleteCommandRunner, CompleteCommandRunner>();
            services.AddTransient<IGetListQueryRunner, GetListQueryRunner>();
            services.AddTransient<IAddResultHandler, AddResultHandler>();
            services.AddTransient<ICompleteResultHandler, CompleteResultHandler>();
            services.AddTransient<IGetListResultHandler, GetListResultHandler>();
            services.AddTransient<IAddCommand, AddCommand>();
            services.AddTransient<IAddCommandValidator, AddCommandValidator>();
            services.AddTransient<IAddCommandArgumentMapper, AddCommandArgumentMapper>();
            services.AddTransient<ICompleteCommand, CompleteCommand>();
            services.AddTransient<ICompleteCommandArgumentMapper, CompleteCommandArgumentMapper>();
            services.AddTransient<IGetListQuery, GetListQuery>();
            services.AddTransient<IListItemMapper, ListItemMapper>();
            services.AddTransient<IReadOnlyRepository, EFReadOnlyRepository>();
            services.AddTransient<IWriteRepository, EFWriteRepository>();
            services.ConfigureDataServices(config.GetConnectionString("ToDoListDB"));

            serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
