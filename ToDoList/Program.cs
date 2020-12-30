using CommandLine;
using ToDoList.Console.Arguments;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Core.Commands.Interfaces;

namespace ToDoList.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IToDoListServiceContainer serviceProvider = new ToDoListServiceContainer())
            {
                Parser.Default.ParseArguments<AddCommandArguments, CompleteCommandArguments>(args)
                    .WithParsed<AddCommandArguments>(arguments =>
                    {
                        var addCommand = serviceProvider.GetService<IAddCommand>();
                        var addCommandMapper = serviceProvider.GetService<IAddCommandArgumentMapper>();
                        addCommand.Execute(addCommandMapper.Map(arguments));
                    })
                    .WithParsed<CompleteCommandArguments>(arguments =>
                    {
                        var completeCommand = serviceProvider.GetService<ICompleteCommand>();
                        var completeCommandMapper = serviceProvider.GetService<ICompleteCommandArgumentMapper>();
                        completeCommand.Execute(completeCommandMapper.Map(arguments));
                    });
            }
        }
    }
}
