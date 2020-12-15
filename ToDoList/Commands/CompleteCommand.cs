using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Core.Commands
{
    public static class CompleteCommand
    {
        public static void CompleteItem(IEnumerable<ListItem> listItems, string input)
        {
            var item = listItems.Single(x => x.Id == int.Parse(input));
            item.Complete();
        }
    }
}
