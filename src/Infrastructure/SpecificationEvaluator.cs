using System.Linq;
using Domain;

namespace Infrastructure
{
    public class SpecificationEvaluator<TEntity>
        where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.IsPagingEnabled)
            {
                var pageNumber = specification.PageNumber > 0
                    ? specification.PageNumber
                    : 1;

                var pageSize = specification.PageSize > 0
                    ? specification.PageSize
                    : 10;

                query = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            return query;
        }
    }
}
