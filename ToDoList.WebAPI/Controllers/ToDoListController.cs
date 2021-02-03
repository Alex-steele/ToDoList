using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IResultResolver<CommandResultWrapper> commandResolver;
        private readonly IResultResolver<QueryResultWrapper> queryResolver;
        private readonly IGetListQuery getListQuery;
        private readonly IAddCommand addCommand;
        private readonly ICompleteCommand completeCommand;
        private readonly IDeleteCommand deleteCommand;

        public ToDoListController(
            IResultResolver<CommandResultWrapper> commandResolver,
            IResultResolver<QueryResultWrapper> queryResolver,
            IGetListQuery getListQuery,
            IAddCommand addCommand,
            ICompleteCommand completeCommand,
            IDeleteCommand deleteCommand)
        {
            this.commandResolver = commandResolver;
            this.queryResolver = queryResolver;
            this.getListQuery = getListQuery;
            this.addCommand = addCommand;
            this.completeCommand = completeCommand;
            this.deleteCommand = deleteCommand;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await getListQuery.ExecuteAsync();

            return queryResolver.Resolve(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddCommandModel model)
        {
            var result = await addCommand.ExecuteAsync(model);

            return commandResolver.Resolve(result);
        }

        [HttpPatch("{ItemId:int}")]
        public async Task<IActionResult> CompleteItem([FromRoute] CompleteCommandModel model)
        {
            var result = await completeCommand.ExecuteAsync(model);

            return commandResolver.Resolve(result);
        }

        [HttpDelete("{ItemId:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute] DeleteCommandModel model)
        {
            var result = await deleteCommand.ExecuteAsync(model);

            return commandResolver.Resolve(result);
        }
    }
}
