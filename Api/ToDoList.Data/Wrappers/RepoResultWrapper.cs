using ToDoList.Data.Wrappers.Enums;

namespace ToDoList.Data.Wrappers
{
    public class RepoResultWrapper<T>
    {
        private RepoResultWrapper(RepoResult result)
        {
            Result = result;
        }

        private RepoResultWrapper(T payload)
        {
            Result = RepoResult.Success;
            Payload = payload;
        }

        /// <summary>
        /// Create a new RepoResultWrapper as success with payload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static RepoResultWrapper<T> Success(T payload) => new RepoResultWrapper<T>(payload);

        /// <summary>
        /// Create a new RepoResultWrapper as NotFound
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static RepoResultWrapper<T> NotFound() => new RepoResultWrapper<T>(RepoResult.NotFound);

        /// <summary>
        /// Create a new RepoResultWrapper as Error
        /// </summary>
        /// <returns></returns>
        public static RepoResultWrapper<T> Error() => new RepoResultWrapper<T>(RepoResult.Error);

        public RepoResult Result { get; }

        public T Payload { get; }
    }
}
