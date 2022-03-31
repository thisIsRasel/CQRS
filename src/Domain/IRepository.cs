using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task InsertAsync(TEntity entity);
    }
}
