using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var result = await getListQuery.ExecuteAsync();

                return queryResolver.Resolve(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddCommandModel model)
        {
            try
            {
                var result = await addCommand.ExecuteAsync(model);

                return commandResolver.Resolve(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> CompleteItem(CompleteCommandModel model)
        {
            try
            {
                var result = await completeCommand.ExecuteAsync(model);

                return commandResolver.Resolve(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(DeleteCommandModel model)
        {
            try
            {
                var result = await deleteCommand.ExecuteAsync(model);

                return commandResolver.Resolve(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
    }
}
