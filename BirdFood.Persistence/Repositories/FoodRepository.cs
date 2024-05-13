using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Entities;
using BirdFood.Domain.Exceptions.Food;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BirdFood.Persistence.Repositories
{
    public class FoodRepository : GenericRepository<Food, Guid>, IFoodRepository
    {
        private readonly BirdFoodDBContext _context;
        public FoodRepository(BirdFoodDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Food> CheckFoodExist(Guid food_id)
        {
            var food = await FindByIdAsync(food_id);
            if (food is null) throw new FoodNotFound(food_id);
            return food;
        }

        public async Task CheckFoodsExistById<T>(IEnumerable<T> foods, Func<T, Guid> idSelector)
        {
            foreach (var food in foods)
            {
                var id = idSelector(food);
                if (await CheckFoodExist(id) is null)
                    throw new FoodNotFound(id);
            }
        }

        public override async Task<Food> FindByIdAsync(Guid id, CancellationToken cancellationToken = default,
            params Expression<Func<Food, object>>[] includeProperties)
        {
            return await GetAll(includeProperties).SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }
    }
}
