using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS
{
    public class QueryBase<TOut> : IQuery<Task<Result<TOut>>>
    {
    }
}
