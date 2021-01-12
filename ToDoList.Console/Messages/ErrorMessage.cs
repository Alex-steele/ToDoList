using System;

namespace ToDoList.Console.Messages
{
    public static class ErrorMessage
    {
        public static void Write(string message)
        {
            FormatConsole.Format(ConsoleColor.Black, ConsoleColor.Red, () => Message.Write(message));
        }
    }
}
