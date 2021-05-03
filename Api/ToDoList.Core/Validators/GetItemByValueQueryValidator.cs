using System.Collections.Generic;
using System.Linq;
using ToDoList.Core.Models;
using ToDoList.Core.Validators.Interfaces;

namespace ToDoList.Core.Validators
{
    public class GetItemByValueQueryValidator : IGetItemByValueQueryValidator
    {
        public ValidationResult Validate(GetItemByValueQueryModel model)
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(model.ItemValue))
            {
                errors.Add(new ValidationError(nameof(AddCommandModel.ItemValue), "item value is required"));
            }

            if (model.ItemValue?.Length > 200)
            {
                errors.Add(new ValidationError(nameof(AddCommandModel.ItemValue), "item value must be 200 characters or less"));
            }

            return errors.Any() ? ValidationResult.Error(errors) : ValidationResult.Success;
        }
    }
}
