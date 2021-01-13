using ToDoList.Console.ResultHandlers.Interfaces;
using ToDoList.Console.Runners.Interfaces;
using ToDoList.Core.Queries.Interfaces;

namespace ToDoList.Console.Runners
{
    public class GetListQueryRunner : IGetListQueryRunner
    {
        private readonly IGetListQuery getListQuery;
        private readonly IGetListResultHandler resultHandler;

        public GetListQueryRunner(IGetListQuery getListQuery,
            IGetListResultHandler resultHandler)
        {
            this.getListQuery = getListQuery;
            this.resultHandler = resultHandler;
        }
        
        public void Run()
        {
            var result = getListQuery.Execute();

            resultHandler.Handle(result);
        }
    }
}
