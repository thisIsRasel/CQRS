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

        public Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
