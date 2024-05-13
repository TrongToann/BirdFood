namespace BirdFood.Contract.DTOs.Shop
{
    public class CreateShopDTO : IShopDTO
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public Guid Account_id { get; set; }
    }
}
