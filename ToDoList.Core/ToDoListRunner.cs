using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Validators.Enums;
using ToDoList.Core.Validators.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Core
{
    public class ToDoListRunner : IToDoListRunner
    {
        private readonly IAddCommand addCommand;
        private readonly ICompleteCommand completeCommand;
        private readonly IGetListQuery getListQuery;
        private readonly IUserInputValidator validator;

        public ToDoListRunner(IAddCommand addCommand,
            ICompleteCommand completeCommand,
            IGetListQuery getListQuery,
            IUserInputValidator validator)
        {
            this.addCommand = addCommand;
            this.completeCommand = completeCommand;
            this.getListQuery = getListQuery;
            this.validator = validator;
        }

        public RunnerResultWrapper<List<ListItemModel>> Execute(string input)
        {
            var validationResult = validator.Validate(input);

            if (validationResult == ValidationResult.Invalid)
            {
                return new RunnerResultWrapper<List<ListItemModel>>
                {
                    Result = RunnerResult.ValidationError
                };
            }

            var listItems = getListQuery.GetList();

            if (listItems == null)
            {
                return new RunnerResultWrapper<List<ListItemModel>>
                {
                    Result = RunnerResult.InvalidOperation
                };
            }

            if (InputIsItemId(listItems, input))
            {
                completeCommand.CompleteItem(int.Parse(input));

                return new RunnerResultWrapper<List<ListItemModel>>
                {
                    Payload = getListQuery.GetList(),
                    Result = RunnerResult.Success
                };
            }

            addCommand.AddItem(input);

            return new RunnerResultWrapper<List<ListItemModel>>
            {
                Payload = getListQuery.GetList(),
                Result = RunnerResult.Success
            };
        }

        private static bool InputIsItemId(IEnumerable<ListItemModel> listItems, string input)
        {
            return int.TryParse(input, out var id) && listItems.Any(x => x.Id == id);
        }
    }
}
