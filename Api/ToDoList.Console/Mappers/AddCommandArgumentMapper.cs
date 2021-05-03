using ToDoList.Console.Arguments;
using ToDoList.Console.Mappers.Interfaces;
using ToDoList.Core.Models;

namespace ToDoList.Console.Mappers
{
    public class AddCommandArgumentMapper : IAddCommandArgumentMapper
    {
        public AddCommandModel Map(AddCommandArguments args)
        {
            return new AddCommandModel
            {
                ItemValue = args.ItemValue
            };
        }
    }
}
