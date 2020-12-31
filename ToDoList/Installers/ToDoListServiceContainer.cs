using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly AutofacServiceProvider serviceProvider;

        public ToDoListServiceContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AddCommand>().As<IAddCommand>();
            builder.RegisterType<CompleteCommand>().As<ICompleteCommand>();
            builder.RegisterType<GetListQuery>().As<IGetListQuery>();
            builder.RegisterType<AddCommandValidator>().As<IAddCommandValidator>();
            builder.RegisterType<ListItemMapper>().As<IListItemMapper>();
            builder.RegisterType<AddCommand>().As<IAddCommand>();
            builder.RegisterType<CompleteCommand>().As<ICompleteCommand>();
            builder.RegisterType<AddCommandArgumentMapper>().As<IAddCommandArgumentMapper>();
            builder.RegisterType<CompleteCommandArgumentMapper>().As<ICompleteCommandArgumentMapper>();
            builder.RegisterType<ToDoListRepository>().As<IToDoListRepository>().InstancePerLifetimeScope();

            builder.Populate(new ServiceCollection());

            var appContainer = builder.Build();

            serviceProvider = new AutofacServiceProvider(appContainer);
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public void Dispose()
        {
            serviceProvider.Dispose();
        }
    }
}
