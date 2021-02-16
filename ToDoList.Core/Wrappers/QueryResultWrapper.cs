using System.Collections.Generic;
using ToDoList.Core.Models;
using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Core.Wrappers
{
    public class QueryResultWrapper
    {
        private QueryResultWrapper(QueryResult result)
        {
            Result = result;
        }

        private QueryResultWrapper(IEnumerable<ListItemModel> payload)
        {
            Payload = payload;
            Result = QueryResult.Success;
        }

        /// <summary>
        /// Create a new QueryResultWrapper as success
        /// </summary>
        /// <param name="payload">Payload of query</param>
        /// <returns></returns>
        public static QueryResultWrapper Success(IEnumerable<ListItemModel> payload) => new QueryResultWrapper(payload);

        /// <summary>
        /// Create a new QueryResultWrapper as NotFound
        /// </summary>
        public static QueryResultWrapper NotFound => new QueryResultWrapper(QueryResult.NotFound);

        /// <summary>
        /// Create a new QueryResultWrapper as Error
        /// </summary>
        public static QueryResultWrapper Error => new QueryResultWrapper(QueryResult.Error);

        public QueryResult Result { get; }

        public IEnumerable<ListItemModel> Payload { get; }
    }
}
