using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Entities;

namespace BirdFood.Persistence.Repositories
{
    public class ShopRepository : GenericRepository<Shop, Guid>, IShopRepository
    {
        private readonly BirdFoodDBContext _context;
        public ShopRepository(BirdFoodDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
