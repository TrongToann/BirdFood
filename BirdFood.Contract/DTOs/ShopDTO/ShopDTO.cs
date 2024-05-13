using BirdFood.Contract.DTOs.BaseDTO;
using BirdFood.Contract.DTOs.Shop;

namespace BirdFood.Contract.DTOs.ShopDTO
{
    public class ShopDTO : Base, IShopDTO
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
    }
}
