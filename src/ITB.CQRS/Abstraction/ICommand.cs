namespace ITB.CQRS.Abstraction
{
    public interface ICommand<TOut> : IRequest<TOut>
    {
    }
}
