using System;

namespace ToDoList.Console.Messages
{
    public static class SuccessMessage
    {
        public static void Write(string message)
        {
            FormatConsole.Format(ConsoleColor.Green, () => System.Console.WriteLine(message));
        }
    }
}
