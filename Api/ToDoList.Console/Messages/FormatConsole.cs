using System;

namespace ToDoList.Console.Messages
{
    public static class FormatConsole
    {
        /// <summary>
        /// Write formatted message to console
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="writeMessage"></param>
        public static void Format(ConsoleColor backgroundColor, ConsoleColor foregroundColor, Action writeMessage)
        {
            System.Console.BackgroundColor = backgroundColor;
            System.Console.ForegroundColor = foregroundColor;
            writeMessage();
            System.Console.ResetColor();
        }

        /// <summary>
        /// Write formatted message to console
        /// </summary>
        /// <param name="foregroundColor"></param>
        /// <param name="writeMessage"></param>
        public static void Format(ConsoleColor foregroundColor, Action writeMessage)
        {
            System.Console.ForegroundColor = foregroundColor;
            writeMessage();
            System.Console.ResetColor();
        }
    }
}
