using CommandLine;

namespace ToDoList.Console.Arguments
{
    [Verb("add", HelpText = "Add an item to your To-do List")]
    public class AddCommandArguments
    {
        [Option('i', "item", Required = true, HelpText = "To-do list item to be added.")]
        public string ItemValue { get; set; }
    }
}
