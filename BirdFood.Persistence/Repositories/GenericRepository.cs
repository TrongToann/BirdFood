using BirdFood.Domain.Abstraction;
using BirdFood.Domain.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BirdFood.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>, IDisposable
        where TEntity : EntityBase<TKey>
    {
        private readonly BirdFoodDBContext _context;

        public GenericRepository(BirdFoodDBContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty).AsNoTracking();
                }
            }
            return items;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty).AsNoTracking();
                }
            }
            return items.Where(predicate);
        }
        public virtual async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await GetAll(includeProperties).SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await GetAll(includeProperties).SingleOrDefaultAsync(predicate, cancellationToken);
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveMultiple(List<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public async Task<List<TEntity>> RawSQL(string sql)
        {
            var result = await _context.Set<TEntity>().FromSqlRaw(sql).ToListAsync();
            return result;
        }

        public async Task<int> CountTotal()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
    }
}
