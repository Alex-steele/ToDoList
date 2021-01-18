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

            services.AddLogging(configuration => configuration.AddConsole()).AddSingleton<ToDoListRunner>();
            services.AddLogging(configuration => configuration.AddConsole()).AddSingleton<EFReadOnlyRepository>();
            services.AddSingleton<IToDoListRunner, ToDoListRunner>();
            services.AddSingleton<IAddCommandRunner, AddCommandRunner>();
            services.AddSingleton<ICompleteCommandRunner, CompleteCommandRunner>();
            services.AddSingleton<IGetListQueryRunner, GetListQueryRunner>();
            services.AddSingleton<IAddResultHandler, AddResultHandler>();
            services.AddSingleton<ICompleteResultHandler, CompleteResultHandler>();
            services.AddSingleton<IGetListResultHandler, GetListResultHandler>();
            services.AddSingleton<IAddCommand, AddCommand>();
            services.AddSingleton<IAddCommandValidator, AddCommandValidator>();
            services.AddSingleton<IAddCommandArgumentMapper, AddCommandArgumentMapper>();
            services.AddSingleton<ICompleteCommand, CompleteCommand>();
            services.AddSingleton<ICompleteCommandArgumentMapper, CompleteCommandArgumentMapper>();
            services.AddSingleton<IGetListQuery, GetListQuery>();
            services.AddSingleton<IListItemMapper, ListItemMapper>();
            services.AddSingleton<IReadOnlyRepository, EFReadOnlyRepository>();
            services.AddSingleton<IWriteRepository, EFWriteRepository>();
            services.ConfigureDataServices(config.GetConnectionString("ToDoListDB"));

            serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
