using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Entities;

namespace BirdFood.Persistence.Repositories
{
    public class TokenUsedRepository : GenericRepository<TokenUsed, Guid>, ITokenUsedRepository
    {
        private readonly BirdFoodDBContext _context;
        public TokenUsedRepository(BirdFoodDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
