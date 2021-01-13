using System;
using ToDoList.Core.Validators;

namespace ToDoList.Console.Messages
{
    public static class ValidationErrorMessage
    {
        public static void Write(ValidationResult validation)
        {
            FormatConsole.Format(ConsoleColor.Black, ConsoleColor.Red, () =>
            {
                System.Console.WriteLine("Invalid input.\nErrors:");
                foreach (var error in validation.Errors)
                {
                    System.Console.WriteLine($"Property: {error.PropertyName}. Error message: {error.ErrorMessage}.");
                }
            });
        }
    }
}