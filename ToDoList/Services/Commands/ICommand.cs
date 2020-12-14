using System.Reactive;
using FluentValidation.Results;

namespace ToDoList.Core.Services.Commands
{
    public interface ICommand<in TIn, TOut>
    {
        CommandResultWrapper<TOut> Execute(TIn input);
    }

    public interface ICommand<in TIn> : ICommand<TIn, Unit>
    {
    }
}
