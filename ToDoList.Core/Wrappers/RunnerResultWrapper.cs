using ToDoList.Core.Wrappers.Enums;

namespace ToDoList.Core.Wrappers
{
    public class RunnerResultWrapper<T>
    {
        public T Payload { get; set; }

        public RunnerResult Result { get; set; }
    }
}
