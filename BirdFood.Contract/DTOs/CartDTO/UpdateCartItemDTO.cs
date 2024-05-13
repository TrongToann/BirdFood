namespace BirdFood.Contract.DTOs.CartDTO
{
    public class UpdateCartItemDTO
    {
        public Guid Food_id { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
    }
}
