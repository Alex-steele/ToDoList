using System;
using ToDoList.Core.Validators;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Wrappers;
using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Core.Wrappers
{
    public class QueryResultWrapper<T>
    {
        private QueryResultWrapper(QueryResult result)
        {
            Result = result;
        }

        private QueryResultWrapper(T payload)
        {
            Payload = payload;
            Result = QueryResult.Success;
        }

        private QueryResultWrapper(ValidationResult validation)
        {
            Validation = validation;
            Result = QueryResult.ValidationError;
        }

        /// <summary>
        /// Create a new QueryResultWrapper as success
        /// </summary>
        /// <param name="payload">Payload of query</param>
        /// <returns></returns>
        public static QueryResultWrapper<T> Success(T payload) => new QueryResultWrapper<T>(payload);


        /// <summary>
        /// Create a new QueryResultWrapper as NotFound
        /// </summary>
        public static QueryResultWrapper<T> NotFound => new QueryResultWrapper<T>(QueryResult.NotFound);


        /// <summary>
        /// Create a new QueryResultWrapper as Error
        /// </summary>
        public static QueryResultWrapper<T> Error => new QueryResultWrapper<T>(QueryResult.Error);


        /// <summary>
        /// Create a new QueryResultWrapper as ValidationError
        /// </summary>
        public static QueryResultWrapper<T> ValidationError(ValidationResult validation) => new QueryResultWrapper<T>(validation);


        /// <summary>
        /// Create a new QueryResultWrapper from the repo result
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="wrapper"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static QueryResultWrapper<T> FromRepoResult<U>(RepoResultWrapper<U> wrapper, Func<U, T> mapper)
        {
            return wrapper.Result switch
            {
                RepoResult.Success => Success(mapper(wrapper.Payload)),
                RepoResult.NotFound => NotFound,
                RepoResult.Error => Error,
                _ => throw new ArgumentOutOfRangeException(nameof(wrapper.Result))
            };
        }

        public QueryResult Result { get; }

        public T Payload { get; }

        public ValidationResult Validation { get; }
    }
}
