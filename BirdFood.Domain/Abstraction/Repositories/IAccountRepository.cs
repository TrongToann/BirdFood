using BirdFood.Domain.Entities;

namespace BirdFood.Domain.Abstraction.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account, Guid>
    {
    }
}
