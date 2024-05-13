namespace BirdFood.Contract.DTOs.CartDTO
{
    public class AddRangeToCartDTO
    {
        public Guid Account_id { get; set; }
        public List<InputFoodDTO> Foods { get; set; }
    }
}
