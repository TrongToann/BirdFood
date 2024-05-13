using BirdFood.Contract.DTOs.BaseDTO;

namespace BirdFood.Contract.DTOs.CartDTO
{
    public class CartFoodDTO : Base, ICartFoodDTO
    {
        public Guid Food_id { get; set; }
        public int Total { get; set; }
        public FoodDTO.FoodDTO Food { get; set; }
    }
}
