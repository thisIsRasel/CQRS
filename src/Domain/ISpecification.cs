using System;
using System.Linq.Expressions;

namespace Domain
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria { get; }

        int PageSize { get; }

        int PageNumber { get; }

        bool IsPagingEnabled { get; }
    }
}
