using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Resolvers
{
    public class QueryResultResolver : IResultResolver<QueryResultWrapper>
    {
        public IActionResult Resolve(QueryResultWrapper resultWrapper)
        {
            return resultWrapper.Result switch
            {
                QueryResult.Success => new OkObjectResult(resultWrapper.Payload),
                QueryResult.NotFound => new NotFoundResult(),
                QueryResult.Error => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
