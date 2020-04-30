using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS
{
    public abstract class HandlerBase<TIn, TOut> : IHandler<TIn, Task<Result<TOut>>>
        where TIn : IRequest<Task<Result<TOut>>>
    {
        public abstract Task<Result<TOut>> Handle(TIn input);
    }
}
