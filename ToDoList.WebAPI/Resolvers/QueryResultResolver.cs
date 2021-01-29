using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ToDoList.Core.Wrappers;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Resolvers
{
    public class QueryResultResolver : ControllerBase, IResultResolver<QueryResultWrapper>
    {
        public IActionResult Resolve(QueryResultWrapper resultWrapper)
        {
            return resultWrapper.Result switch
            {
                QueryResult.Success => Ok(resultWrapper.Payload),
                QueryResult.Error => StatusCode(StatusCodes.Status500InternalServerError, "Database failure"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
