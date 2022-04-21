using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetItemAsync(string itemId);

        Task<IEnumerable<TEntity>> GetItemsAsync(
            ISpecification<TEntity> specification = null);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
