using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.Queries.Interfaces;
using ToDoList.Core.Wrappers;
using ToDoList.WebAPI.Resolvers.Interfaces;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/ToDoList")]
    [ApiController]
    public class ToDoListReadController : ControllerBase
    {
        private readonly IResultResolver<QueryResultWrapper<List<ListItemModel>>> queryListResolver;
        private readonly IResultResolver<QueryResultWrapper<ListItemModel>> queryItemResolver;
        private readonly IGetListQuery getListQuery;
        private readonly IGetItemByIdQuery getItemByIdQuery;
        private readonly IGetItemByValueQuery getItemByValueQuery;
        private readonly IGetItemByValueFuzzyQuery getItemByValueFuzzyQuery;
        private readonly IGetItemsByDateQuery getItemsByDateQuery;

        public ToDoListReadController(
            IResultResolver<QueryResultWrapper<List<ListItemModel>>> queryListResolver,
            IResultResolver<QueryResultWrapper<ListItemModel>> queryItemResolver,
            IGetListQuery getListQuery,
            IGetItemByIdQuery getItemByIdQuery,
            IGetItemByValueQuery getItemByValueQuery,
            IGetItemByValueFuzzyQuery getItemByValueFuzzyQuery,
            IGetItemsByDateQuery getItemsByDateQuery)
        {
            this.queryListResolver = queryListResolver;
            this.queryItemResolver = queryItemResolver;
            this.getListQuery = getListQuery;
            this.getItemByIdQuery = getItemByIdQuery;
            this.getItemByValueQuery = getItemByValueQuery;
            this.getItemByValueFuzzyQuery = getItemByValueFuzzyQuery;
            this.getItemsByDateQuery = getItemsByDateQuery;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await getListQuery.ExecuteAsync();

            return queryListResolver.Resolve(result);
        }

        [HttpGet("{ItemId:int}")]
        public async Task<IActionResult> GetItemById([FromRoute] GetItemByIdQueryModel model)
        {
            var result = await getItemByIdQuery.ExecuteAsync(model);

            return queryItemResolver.Resolve(result);
        }

        [HttpGet("searchByValue")]
        public async Task<IActionResult> GetItemByValue([FromQuery] GetItemByValueQueryModel model, bool fuzzy = false)
        {
            var result = fuzzy
                ? await getItemByValueFuzzyQuery.ExecuteAsync(model)
                : await getItemByValueQuery.ExecuteAsync(model);

            return queryListResolver.Resolve(result);
        }

        [HttpGet("searchByDate")]
        public async Task<IActionResult> GetItemByDate([FromQuery] GetItemByDateQueryModel model)
        {
            var result = await getItemsByDateQuery.ExecuteAsync(model);

            return queryListResolver.Resolve(result);
        }
    }
}
