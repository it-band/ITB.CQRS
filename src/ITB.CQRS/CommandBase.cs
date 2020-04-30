using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS
{
    public class CommandBase<TOut> : ICommand<Task<Result<TOut>>>
    {
    }

    public class CommandBase : ICommand<Task<Result>>
    {

    }
}
