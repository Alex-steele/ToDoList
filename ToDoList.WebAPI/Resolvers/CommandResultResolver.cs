using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Resolvers
{
    public class CommandResultResolver : ControllerBase, IResultResolver<CommandResultWrapper>
    {
        public IActionResult Resolve(CommandResultWrapper resultWrapper)
        {
            return resultWrapper.Result switch
            {
                CommandResult.Success => Ok(),
                CommandResult.Created => StatusCode(StatusCodes.Status201Created),
                CommandResult.ValidationError => BadRequest(resultWrapper.Validation),
                CommandResult.NotFound => NotFound(),
                CommandResult.Error => StatusCode(StatusCodes.Status500InternalServerError, "Database failure"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}