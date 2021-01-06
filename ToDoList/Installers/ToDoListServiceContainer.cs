using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Core.Commands;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Mappers;
using ToDoList.Core.Mappers.Interfaces;
using ToDoList.Core.Queries;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Data.Repositories;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Console.Installers
{
    public class ToDoListServiceContainer : IToDoListServiceContainer
    {
        private readonly IServiceProvider serviceProvider;

        public ToDoListServiceContainer()
        {
            var services = new ServiceCollection();

            services.AddLogging(config => config.AddConsole()).AddTransient<Program>();
            services.AddTransient<IAddCommand, AddCommand>();
            services.AddTransient<IAddCommandValidator, AddCommandValidator>();
            services.AddTransient<IAddCommandArgumentMapper, AddCommandArgumentMapper>();
            services.AddTransient<ICompleteCommand, CompleteCommand>();
            services.AddTransient<ICompleteCommandArgumentMapper, CompleteCommandArgumentMapper>();
            services.AddTransient<IGetListQuery, GetListQuery>();
            services.AddTransient<IListItemMapper, ListItemMapper>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();

            serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
