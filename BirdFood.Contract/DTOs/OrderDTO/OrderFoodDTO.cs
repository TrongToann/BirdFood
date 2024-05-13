namespace BirdFood.Contract.DTOs.OrderDTO
{
    public class OrderFoodDTO
    {
        public Guid Food_id { get; set; }
        public FoodDTO.FoodDTO Food { get; set; }
        public int TotalFood { get; set; }
    }
}
