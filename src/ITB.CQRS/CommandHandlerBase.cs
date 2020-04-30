using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS
{
    public abstract class CommandHandlerBase<TIn, TOut> : ICommandHandler<TIn, Task<Result<TOut>>>
        where TIn : ICommand<Task<Result<TOut>>>
    {
        public abstract Task<Result<TOut>> Handle(TIn input);
    }

    public abstract class CommandHandlerBase<TIn> : ICommandHandler<TIn, Task<Result>>
        where TIn : ICommand<Task<Result>>
    {
        public abstract Task<Result> Handle(TIn input);
    }
}
