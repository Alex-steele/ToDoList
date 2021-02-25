using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;
using ToDoList.Core.Validators;
using ToDoList.Core.Wrappers.Enums;
using ToDoList.Data.Entities;
using ToDoList.Data.Wrappers;
using ToDoList.Data.Wrappers.Enums;

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
        public static QueryResultWrapper Success(IEnumerable<ListItemModel> payload) => new QueryResultWrapper(payload);


        /// <summary>
        /// Create a new QueryResultWrapper as NotFound
        /// </summary>
        public static QueryResultWrapper NotFound => new QueryResultWrapper(QueryResult.NotFound);


        /// <summary>
        /// Create a new QueryResultWrapper as Error
        /// </summary>
        public static QueryResultWrapper Error => new QueryResultWrapper(QueryResult.Error);


        /// <summary>
        /// Create a new QueryResultWrapper as ValidationError
        /// </summary>
        public static QueryResultWrapper ValidationError(ValidationResult validation) => new QueryResultWrapper(validation);


        /// <summary>
        /// Create a new QueryResultWrapper from the repo result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wrapper"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static QueryResultWrapper FromRepoResult<T>(RepoResultWrapper<T> wrapper, Func<T, IEnumerable<ListItemModel>> mapper)
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

        public IEnumerable<ListItemModel> Payload { get; }

        public ValidationResult Validation { get; }
    }
}
