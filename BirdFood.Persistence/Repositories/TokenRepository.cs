using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Entities;

namespace BirdFood.Persistence.Repositories
{
    public class TokenRepository : GenericRepository<Token, Guid>, ITokenRepository
    {
        private readonly BirdFoodDBContext _context;

        public TokenRepository(BirdFoodDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
