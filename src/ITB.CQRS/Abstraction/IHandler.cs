namespace ITB.CQRS.Abstraction
{
    public interface IHandler<in TIn, out TOut>
        where TIn : IRequest<TOut>
    {
        TOut Handle(TIn input);
    }
}
