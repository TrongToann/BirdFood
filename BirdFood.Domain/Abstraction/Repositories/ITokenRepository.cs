using BirdFood.Domain.Entities;

namespace BirdFood.Domain.Abstraction.Repositories
{
    public interface ITokenRepository : IGenericRepository<Token, Guid>
    {
    }
}
