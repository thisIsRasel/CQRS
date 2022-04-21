using System;
using System.Linq.Expressions;

namespace Domain
{
    public class BaseSpecification<TEntity>
        : ISpecification<TEntity> where TEntity : class
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void ApplyPaging(int pageNumber, int pageSize = 10)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            IsPagingEnabled = true;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public bool IsPagingEnabled { get; private set; }
    }
}
