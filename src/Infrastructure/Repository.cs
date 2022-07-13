using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _entities;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<TEntity>();
        }

        public async Task<TEntity?> GetItemAsync(string itemId)
        {
            return await _entities.FindAsync(itemId);
        }

        public Task<IEnumerable<TEntity>> GetItemsAsync(
            ISpecification<TEntity>? specification = null)
        {
            var result = (IEnumerable<TEntity>)ApplySpecification(specification);
            return Task.FromResult(result);
        }

        public Task<int> CountAsync(
            ISpecification<TEntity>? specification = null)
        {
            var query = ApplySpecification(specification);
            return Task.FromResult(query.Count());
        }

        public async Task InsertAsync(TEntity entity)
        {
            _entities.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _entities.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _entities.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> ApplySpecification(
            ISpecification<TEntity>? specification)
        {
            if (specification is null)
            {
                return _entities.AsQueryable();
            }

            return SpecificationEvaluator<TEntity>
                .GetQuery(_entities.AsQueryable(), specification);
        }
    }
}
