using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.CartDTO;

namespace BirdFood.Contract.Service.Cart
{
    public static class Command
    {
        public record AddToCart(AddToCartDTO addToCart) : ICommand<BaseResponse>;
        public record UpdateCartItem(Guid Cart_id, UpdateCartItemDTO UpdateCartItemDTO) 
            : ICommand<Response>;
        public record AddRangeToCart(AddRangeToCartDTO AddRangeToCartDTO) : ICommand;
    }
}
