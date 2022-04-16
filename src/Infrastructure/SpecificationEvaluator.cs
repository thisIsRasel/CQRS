﻿using System.Linq;
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

            return query;
        }
    }
}
