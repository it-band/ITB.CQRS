using System.Collections.Generic;
using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.CQRS.Models;
using ITB.ResultModel;

namespace ITB.CQRS.Decorators
{
    public class PermissionValidationHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TIn : IRequest<Task<Result<TOut>>>
    {
        private readonly IEnumerable<IPermissionValidator<TIn>> _permissionValidators;

        public PermissionValidationHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated, IEnumerable<IPermissionValidator<TIn>> permissionValidators) : base(decorated)
        {
            _permissionValidators = permissionValidators;
        }

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            foreach (var permissionValidator in _permissionValidators)
            {
                var result = await permissionValidator.Validate(input);

                if (!result.IsValid)
                {
                    return Result.Forbidden(result.ToString());
                }
            }

            return await Decorated.Handle(input);
        }
    }

    public class PermissionValidationHandlerDecorator<TIn> : HandlerDecoratorBase<TIn>
        where TIn : IRequest<Task<Result>>
    {
        private readonly IEnumerable<IPermissionValidator<TIn>> _permissionValidators;

        public PermissionValidationHandlerDecorator(IHandler<TIn, Task<Result>> decorated, IEnumerable<IPermissionValidator<TIn>> permissionValidators) : base(decorated)
        {
            _permissionValidators = permissionValidators;
        }

        public override async Task<Result> Handle(TIn input)
        {
            foreach (var permissionValidator in _permissionValidators)
            {
                var result = await permissionValidator.Validate(input);

                if (!result.IsValid)
                {
                    return Result.Forbidden(result.ToString());
                }
            }

            return await Decorated.Handle(input);
        }
    }
}
