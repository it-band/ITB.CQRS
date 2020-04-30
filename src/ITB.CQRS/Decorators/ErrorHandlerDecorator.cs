using System;
using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;

namespace ITB.CQRS.Decorators
{
    public class ErrorHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TIn : IRequest<Task<Result<TOut>>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decorated"></param>
        public ErrorHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated) : base(decorated)
        {
        }

        /// <summary>
        /// Decorated Handle
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<Result<TOut>> Handle(TIn input)
        {
            try
            {
                return await Decorated.Handle(input);
            }
            catch (Exception ex)
            {
                return new ExceptionFailure(ex);
            }
        }
    }

    public class ErrorHandlerDecorator<TIn> : HandlerDecoratorBase<TIn>
        where TIn : IRequest<Task<Result>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decorated"></param>
        public ErrorHandlerDecorator(IHandler<TIn, Task<Result>> decorated) : base(decorated)
        {
        }

        /// <summary>
        /// Decorated Handle
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<Result> Handle(TIn input)
        {
            try
            {
                return await Decorated.Handle(input);
            }
            catch (Exception ex)
            {
                return new ExceptionFailure(ex);
            }
        }
    }
}
