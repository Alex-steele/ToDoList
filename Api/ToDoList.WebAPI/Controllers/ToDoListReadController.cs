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
        private readonly IResultResolver<QueryResultWrapper> queryResolver;
        private readonly IGetListQuery getListQuery;
        private readonly IGetItemByValueQuery getItemByValueQuery;
        private readonly IGetItemByValueFuzzyQuery getItemByValueFuzzyQuery;
        private readonly IGetItemsByDateQuery getItemsByDateQuery;

        public ToDoListReadController(
            IResultResolver<QueryResultWrapper> queryResolver,
            IGetListQuery getListQuery,
            IGetItemByValueQuery getItemByValueQuery,
            IGetItemByValueFuzzyQuery getItemByValueFuzzyQuery,
            IGetItemsByDateQuery getItemsByDateQuery)
        {
            this.queryResolver = queryResolver;
            this.getListQuery = getListQuery;
            this.getItemByValueQuery = getItemByValueQuery;
            this.getItemByValueFuzzyQuery = getItemByValueFuzzyQuery;
            this.getItemsByDateQuery = getItemsByDateQuery;
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
    }
}
