using BirdFood.Contract.DTOs.Shop;
using BirdFood.Contract.DTOs.AccountDTO;

namespace BirdFood.Contract.Service.Shop
{
    public class Response : IShopDTO
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public virtual AccountDTO Account { get; set; }
    }
}
