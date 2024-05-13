namespace BirdFood.Contract.DTOs.CartDTO
{
    public class AddToCartDTO
    {
        public Guid Account_id { get; set; }
        public InputFoodDTO? InputFoodDTO { get; set; }
    }
    public class InputFoodDTO
    {
        public Guid Food_id { get; set; }
        public int Total { get; set; } = 1;
    }
}
