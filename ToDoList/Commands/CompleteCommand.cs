using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Core.Commands
{
    public static class CompleteCommand
    {
        public static void CompleteItem(IEnumerable<ListItem> listItems, int id)
        {
            var item = listItems.Single(x => x.Id == id);
            item.Complete();
        }
    }
}
