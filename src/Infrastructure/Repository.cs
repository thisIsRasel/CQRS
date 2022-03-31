using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<TEntity> _entities;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _entities.ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            _entities.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
