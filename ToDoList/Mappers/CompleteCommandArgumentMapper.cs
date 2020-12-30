using ToDoList.Console.Arguments;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Core.Models;

namespace ToDoList.Console.Mappers
{
    public class CompleteCommandArgumentMapper : ICompleteCommandArgumentMapper
    {
        public CompleteCommandModel Map(CompleteCommandArguments args)
        {
            return new CompleteCommandModel
            {
                ItemId = args.ItemId
            };
        }
    }
}
