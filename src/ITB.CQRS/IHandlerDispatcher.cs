using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS
{
    public interface IHandlerDispatcher
    {
        Task<Result<TOut>> Handle<TIn, TOut>(TIn input)
            where TIn : IRequest<Task<Result<TOut>>>;

        Task<Result> Handle<TIn>(TIn input)
            where TIn : CommandBase;
    }
}
