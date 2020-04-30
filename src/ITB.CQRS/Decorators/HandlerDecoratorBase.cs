using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS.Decorators
{
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, Task<Result<TOut>>>
        where TIn : IRequest<Task<Result<TOut>>>
    {
        protected IHandler<TIn, Task<Result<TOut>>> Decorated { get; }

        protected HandlerDecoratorBase(IHandler<TIn, Task<Result<TOut>>> decorated)
        {
            Decorated = decorated;
        }

        public abstract Task<Result<TOut>> Handle(TIn input);
    }

    public abstract class HandlerDecoratorBase<TIn> : IHandler<TIn, Task<Result>>
        where TIn : IRequest<Task<Result>>
    {
        protected IHandler<TIn, Task<Result>> Decorated { get; }

        protected HandlerDecoratorBase(IHandler<TIn, Task<Result>> decorated)
        {
            Decorated = decorated;
        }

        public abstract Task<Result> Handle(TIn input);
    }
}
