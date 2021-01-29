using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDoList.Core.Commands.Interfaces;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IGetListQuery getListQuery;
        private readonly IAddCommand addCommand;
        private readonly ICompleteCommand completeCommand;
        private readonly IDeleteCommand deleteCommand;

        public ToDoListController(IGetListQuery getListQuery,
            IAddCommand addCommand,
            ICompleteCommand completeCommand,
            IDeleteCommand deleteCommand)
        {
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

                return result.Result == QueryResult.Success
                    ? Ok(result.Payload)
                    : StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
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

                return result.Result switch
                {
                    CommandResult.Success => Ok(),
                    CommandResult.ValidationError => BadRequest(),
                    CommandResult.NotFound => NotFound(),
                    CommandResult.Error => StatusCode(StatusCodes.Status500InternalServerError, "Database failure"),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpPut]
        public async Task<IActionResult> CompleteItem(CompleteCommandModel model)
        {
            try
            {
                var result = await completeCommand.ExecuteAsync(model);

                return result.Result switch
                {
                    CommandResult.Success => Ok(),
                    CommandResult.ValidationError => BadRequest(),
                    CommandResult.NotFound => NotFound(),
                    CommandResult.Error => StatusCode(StatusCodes.Status500InternalServerError, "Database failure"),
                    _ => throw new ArgumentOutOfRangeException()
                };
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

                return result.Result switch
                {
                    CommandResult.Success => Ok(),
                    CommandResult.ValidationError => BadRequest(),
                    CommandResult.NotFound => NotFound(),
                    CommandResult.Error => StatusCode(StatusCodes.Status500InternalServerError, "Database failure"),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
    }
}
