namespace BirdFood.Contract.DTOs.Shop
{
    public class UpdateShopDTO : IShopDTO
    {
        public string Name {  get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
    }
}
