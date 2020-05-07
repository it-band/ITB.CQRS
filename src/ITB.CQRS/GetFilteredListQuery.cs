using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITB.CQRS.Abstraction;
using ITB.CQRS.Models;
using ITB.Repository.Abstraction;
using ITB.ResultModel;
using ITB.Shared.Domain.Entities;
using ITB.Specification;
using Microsoft.EntityFrameworkCore;

namespace ITB.CQRS
{
    public abstract class GetFilteredListQuery<TOut> : FilterBase, IQuery<Task<Result<PagedList<TOut>>>>
    {
    }

    public abstract class GetFilteredListQueryHandler<TIn, TOut, TEntity> : QueryHandlerBase<TIn, PagedList<TOut>>
        where TIn : GetFilteredListQuery<TOut>
        where TEntity : class, IEntity
    {
        protected readonly IMapper Mapper;
        protected Specification<TEntity> Specification { get; set; } = new DefaultSpecification<TEntity>();

        protected readonly IReadRepository<TEntity> Repository;

        protected GetFilteredListQueryHandler(IMapper mapper, IReadRepository<TEntity> repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        protected virtual void BuildSpecification(TIn input)
        {
        }

        protected virtual void Sort(Sorting sorting)
        {
        }

        public override async Task<Result<PagedList<TOut>>> Handle(TIn input)
        {
            BuildSpecification(input);

            var query = Repository.Query(Specification);

            Sort(input.Sorting);

            int? totalCount = null;

            if (input.Paging.ItemsCount > 0)
            {
                totalCount = await query.CountAsync();
            }

            query = query.ApplyPageFilter(input);

            var items = await query.ProjectTo<TOut>(Mapper.ConfigurationProvider).ToListAsync();

            return new PagedList<TOut>(items, totalCount);
        }
    }
}
