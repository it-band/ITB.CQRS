using System.Threading.Tasks;
using ITB.CQRS.Abstraction;
using ITB.ResultModel;
using Microsoft.EntityFrameworkCore;

namespace ITB.CQRS.Decorators
{
    public class TransactionHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
            where TIn : CommandBase<TOut>
    {
        private readonly DbContext _dbContext;

        public TransactionHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated, DbContext dbContext) : base(decorated)
        {
            _dbContext = dbContext;
        }

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var result = await Decorated.Handle(input);

            if (result.IsSuccess)
            {
                await transaction.CommitAsync();
            }

            return result;
        }
    }

    public class TransactionHandlerDecorator<TIn> : HandlerDecoratorBase<TIn>
        where TIn : CommandBase
    {
        private readonly DbContext _dbContext;

        public TransactionHandlerDecorator(IHandler<TIn, Task<Result>> decorated, DbContext dbContext) : base(decorated)
        {
            _dbContext = dbContext;
        }

        public override async Task<Result> Handle(TIn input)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var result = await Decorated.Handle(input);

            if (result.IsSuccess)
            {
                await transaction.CommitAsync();
            }

            return result;
        }
    }
}
