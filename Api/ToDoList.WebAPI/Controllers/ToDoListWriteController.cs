using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/ToDoList")]
    [ApiController]
    public class ToDoListWriteController : ControllerBase
    {
        private readonly IResultResolver<CommandResultWrapper> commandResolver;
        private readonly IAddCommand addCommand;
        private readonly ICompleteCommand completeCommand;
        private readonly IDeleteCommand deleteCommand;

        public ToDoListWriteController(
            IResultResolver<CommandResultWrapper> commandResolver,
            IAddCommand addCommand,
            ICompleteCommand completeCommand,
            IDeleteCommand deleteCommand)
        {
            this.commandResolver = commandResolver;
            this.addCommand = addCommand;
            this.completeCommand = completeCommand;
            this.deleteCommand = deleteCommand;
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
