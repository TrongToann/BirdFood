namespace BirdFood.Domain.Exceptions.Cart
{
    public class CartNotFound : NotFoundException
    {
        public CartNotFound(Guid Cart_id) :
            base($"Cart with id {Cart_id} was not found")
        {
        }
    }
}
