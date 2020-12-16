using System.Linq;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.Core.Commands
{
    public class AddCommand : IAddCommand
    {
        private readonly IToDoListRepository repository;

        public AddCommand(IToDoListRepository repository)
        {
            this.repository = repository;
        }

        public void AddItem(string itemValue)
        {
            var listItems = repository.GetAll();

            repository.Add(new ListItem
            {
                Id = listItems.Max(x => x?.Id) + 1 ?? 1,
                Value = itemValue,
                Completed = false
            });
        }
    }
}
