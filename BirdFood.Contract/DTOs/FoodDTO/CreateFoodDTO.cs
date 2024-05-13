
namespace BirdFood.Contract.DTOs.FoodDTO
{
    public class CreateFoodDTO : IFoodDTO
    {
        public Guid Shop_id { get; set; }
        public string Name { get ; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int In_stock { get; set; }
    }
}
