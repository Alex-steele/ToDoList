using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Core.Commands
{
    public static class AddCommand
    {
        public static void AddItem(List<ListItem> listItems, string itemValue)
        {
            listItems.Add(new ListItem
            {
                Id = listItems.Max(x => x?.Id) + 1 ?? 1,
                Value = itemValue,
                Completed = false
            });
        }
    }
}
