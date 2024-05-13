namespace BirdFood.Domain.Exceptions.Shop
{
    public class ShopNotFound : NotFoundException
    {
        public ShopNotFound(Guid shopId) :
            base($"Shop with id {shopId} was not found")
        {
        }
    }
}
