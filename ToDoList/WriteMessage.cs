using System;
using ToDoList.Core.Validators;

namespace ToDoList.Console
{
    public static class WriteMessage
    {
        public static void Success(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        public static void Error(string message)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        public static void ValidationError(ValidationResult validation)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Invalid input.\nErrors:");
            foreach (var error in validation.Errors)
            {
                System.Console.WriteLine($"Property: {error.PropertyName}. Error message: {error.Error}.");
            }
            System.Console.ResetColor();
        }
    }
}
