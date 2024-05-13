namespace BirdFood.Domain.Exceptions.Food
{
    public class FoodNotFound : NotFoundException
    {
        public FoodNotFound(Guid foodId) :
            base($"Food with id {foodId} was not found")
        {
        }
}
}
