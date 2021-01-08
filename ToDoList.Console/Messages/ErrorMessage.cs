using System;

namespace ToDoList.Console.Messages
{
    public static class ErrorMessage
    {
        public static void Write(string message)
        {
            FormatConsole.Format(ConsoleColor.Black, ConsoleColor.Red, () => System.Console.WriteLine(message));
        }
    }
}
