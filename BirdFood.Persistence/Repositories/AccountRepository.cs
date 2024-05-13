using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Entities;

namespace BirdFood.Persistence.Repositories
{
    public class AccountRepository : GenericRepository<Account, Guid>, IAccountRepository
    {
        private readonly BirdFoodDBContext _context;
        public AccountRepository(BirdFoodDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
