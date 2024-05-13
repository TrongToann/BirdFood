using BirdFood.Domain.Entities;

namespace BirdFood.Domain.Abstraction.Repositories
{
    public interface IFoodRepository : IGenericRepository<Food, Guid>
    {
        Task<Food> CheckFoodExist(Guid food_id);
        Task CheckFoodsExistById<T>(IEnumerable<T> foods, Func<T, Guid> idSelector);
    }
}
