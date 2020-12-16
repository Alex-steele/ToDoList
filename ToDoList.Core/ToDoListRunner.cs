﻿using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;

namespace ToDoList.Core
{
    public class ToDoListRunner
    {
        private readonly IAddCommand addCommand;
        private readonly ICompleteCommand completeCommand;
        private readonly IGetListQuery getListQuery;

        public ToDoListRunner(IAddCommand addCommand,
            ICompleteCommand completeCommand,
            IGetListQuery getListQuery)
        {
            this.addCommand = addCommand;
            this.completeCommand = completeCommand;
            this.getListQuery = getListQuery;
        }

        public RunnerResultWrapper<List<ListItem>> Execute(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) //-----Validation
            {
                return new RunnerResultWrapper<List<ListItem>>
                {
                    Result = RunnerResult.ValidationError
                };
            }

            var listItems = getListQuery.GetList();

            if (listItems == null)
            {
                return new RunnerResultWrapper<List<ListItem>>
                {
                    Result = RunnerResult.InvalidOperation
                };
            }

            if (InputIsItemId(listItems, input))
            {
                completeCommand.CompleteItem(int.Parse(input));

                return new RunnerResultWrapper<List<ListItem>>
                {
                    Payload = listItems,
                    Result = RunnerResult.Success
                };
            }

            addCommand.AddItem(input);

            return new RunnerResultWrapper<List<ListItem>>
            {
                Payload = listItems,
                Result = RunnerResult.Success
            };
        }

        private static bool InputIsItemId(IEnumerable<ListItem> listItems, string input)
        {
            return int.TryParse(input, out var id) && listItems.Any(x => x.Id == id);
        }
    }
}
