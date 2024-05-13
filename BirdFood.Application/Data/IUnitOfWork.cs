using BirdFood.Domain.Abstraction;
using BirdFood.Domain.Abstraction.Repositories;

namespace BirdFood.Application.Data
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : EntityBase<TKey>;
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(string Username);
    }
}
