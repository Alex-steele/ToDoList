using System.Collections.Generic;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core
{
    public interface IToDoListRunner
    {
        RunnerResultWrapper<List<ListItemModel>> Execute(string input);
    }
}