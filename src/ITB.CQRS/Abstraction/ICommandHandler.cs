namespace ITB.CQRS.Abstraction
{
    public interface ICommandHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : ICommand<TOut>
    {
    }
}
