﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetItemAsync(string itemId);

        Task<IEnumerable<TEntity>> GetItemsAsync();

        Task<IEnumerable<TEntity>> GetItemsAsync(
            ISpecification<TEntity> specification);

        Task InsertAsync(TEntity entity);
    }
}
