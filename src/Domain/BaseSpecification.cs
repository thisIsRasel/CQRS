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

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }
    }
}
