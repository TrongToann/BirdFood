using System.Linq.Expressions;

namespace BirdFood.Domain.Abstraction.Repositories
{
    public interface IGenericRepository<TEntity, in TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includeProperties);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveMultiple(List<TEntity> entities);
        Task<List<TEntity>> RawSQL(string sql);
        Task<int> CountTotal();
    }
}
