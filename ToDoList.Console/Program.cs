using System;
using ToDoList.Console.Installers;
using ToDoList.Console.Installers.Interfaces;
using ToDoList.Console.Messages;
using ToDoList.Console.Runner.Interface;

namespace ToDoList.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting App...");

            try
            {
                IToDoListServiceContainer serviceProvider = new ToDoListServiceContainer();

                var runner = serviceProvider.GetService<IToDoListRunner>();

                runner.Run(serviceProvider, args);
            }
            catch (Exception ex)
            {
                ErrorMessage.Write($"An unhandled error occurred: {ex}");
            }

            System.Console.WriteLine("Press any key to close");

            System.Console.ReadKey();
        }
    }
}
