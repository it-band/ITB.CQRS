using System;
using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ITB.CQRS.Decorators
{
    public class ErrorHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TIn : IRequest<Task<Result<TOut>>>
    {
        private readonly CQRSOptions _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public ErrorHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated, IOptions<CQRSOptions> options, ILogger logger) : base(decorated)
        {
            _logger = logger;
            _options = options.Value;
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
                return _options.ExceptionHandler(ex, _logger);
            }
        }
    }

    public class ErrorHandlerDecorator<TIn> : HandlerDecoratorBase<TIn>
        where TIn : IRequest<Task<Result>>
    {
        private readonly CQRSOptions _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public ErrorHandlerDecorator(IHandler<TIn, Task<Result>> decorated, IOptions<CQRSOptions> options, ILogger logger) : base(decorated)
        {
            _logger = logger;
            _options = options.Value;
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
                return _options.ExceptionHandler(ex, _logger);
            }
        }
    }
}
