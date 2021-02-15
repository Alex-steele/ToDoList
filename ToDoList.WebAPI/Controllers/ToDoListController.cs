using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ToDoListController> logger;
        private readonly IResultResolver<CommandResultWrapper> commandResolver;
        private readonly IResultResolver<QueryResultWrapper> queryResolver;
        private readonly IGetListQuery getListQuery;
        private readonly IGetItemByValueQuery getItemByValueQuery;
        private readonly IGetItemByValueFuzzyQuery getItemByValueFuzzyQuery;
        private readonly IGetItemsByDateQuery getItemsByDateQuery;
        private readonly IAddCommand addCommand;
        private readonly ICompleteCommand completeCommand;
        private readonly IDeleteCommand deleteCommand;

        public ToDoListController(
            ILogger<ToDoListController> logger,
            IResultResolver<CommandResultWrapper> commandResolver,
            IResultResolver<QueryResultWrapper> queryResolver,
            IGetListQuery getListQuery,
            IGetItemByValueQuery getItemByValueQuery,
            IGetItemByValueFuzzyQuery getItemByValueFuzzyQuery,
            IGetItemsByDateQuery getItemsByDateQuery,
            IAddCommand addCommand,
            ICompleteCommand completeCommand,
            IDeleteCommand deleteCommand)
        {
            this.logger = logger;
            this.commandResolver = commandResolver;
            this.queryResolver = queryResolver;
            this.getListQuery = getListQuery;
            this.getItemByValueQuery = getItemByValueQuery;
            this.getItemByValueFuzzyQuery = getItemByValueFuzzyQuery;
            this.getItemsByDateQuery = getItemsByDateQuery;
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

        [HttpGet("searchByValue")]
        public async Task<IActionResult> GetItemByValue([FromQuery] GetItemByValueQueryModel model, bool fuzzy = false)
        {
            var result = fuzzy
                ? await getItemByValueFuzzyQuery.ExecuteAsync(model)
                : await getItemByValueQuery.ExecuteAsync(model);

            return queryResolver.Resolve(result);
        }

        [HttpGet("searchByDate")]
        public async Task<IActionResult> GetItemByDate([FromQuery] GetItemByDateQueryModel model)
        {
            var result = await getItemsByDateQuery.ExecuteAsync(model);

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
