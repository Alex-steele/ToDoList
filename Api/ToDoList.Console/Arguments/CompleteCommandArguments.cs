using CommandLine;

namespace ToDoList.Console.Arguments
{
    [Verb("complete", HelpText = "Mark an item as completed")]
    public class CompleteCommandArguments
    {
        [Option('d', "id", Required = true, HelpText = "To-do list item id to be completed")]
        public int ItemId { get; set; }
    }
}
