using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS
{
    public abstract class QueryHandlerBase<TIn, TOut> : IQueryHandler<TIn, Task<Result<TOut>>>
        where TIn : IQuery<Task<Result<TOut>>>
    {
        public abstract Task<Result<TOut>> Handle(TIn input);
    }
}
