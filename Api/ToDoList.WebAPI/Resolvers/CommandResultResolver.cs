using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Resolvers
{
    public class CommandResultResolver : IResultResolver<CommandResultWrapper>
    {
        public IActionResult Resolve(CommandResultWrapper resultWrapper)
        {
            return resultWrapper.Result switch
            {
                CommandResult.Success => new OkResult(),
                CommandResult.Created => new CreatedResult(string.Empty, resultWrapper.Payload),
                CommandResult.ValidationError => new BadRequestObjectResult(resultWrapper.Validation),
                CommandResult.NotFound => new NotFoundResult(),
                CommandResult.Error => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}