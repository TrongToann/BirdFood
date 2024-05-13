using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.Shop;

namespace BirdFood.Contract.Service.Shop
{
    public static class Command
    {
        public record CreateShop(CreateShopDTO CreateShopDTO) : ICommand<BaseResponse> { }
        public record UpdateShop(Guid ShopId, UpdateShopDTO UpdateShopDTO) : ICommand<BaseResponse> { }
    }
}
