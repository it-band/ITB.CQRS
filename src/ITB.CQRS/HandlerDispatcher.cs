using System;
using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;
using SimpleInjector;

namespace ITB.CQRS
{
    public class HandlerDispatcher : IHandlerDispatcher
    {
        private readonly Container _container;

        public HandlerDispatcher(Container container)
        {
            _container = container;
        }

        public async Task<Result> Handle<TIn>(TIn input)
            where TIn : CommandBase
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var handler = (IHandler<TIn, Task<Result>>)_container.GetInstance(typeof(IHandler<TIn, Task<Result>>));

            return await handler.Handle(input);
        }

        public async Task<Result<TOut>> Handle<TIn, TOut>(TIn input)
            where TIn : IRequest<Task<Result<TOut>>>
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var handler = (IHandler<TIn, Task<Result<TOut>>>)_container.GetInstance(typeof(IHandler<TIn, Task<Result<TOut>>>));

            return await handler.Handle(input);
        }
    }
}
