namespace BirdFood.Contract.DTOs.CartDTO
{
    public interface ICartFoodDTO
    {
        public Guid Food_id { get; set; }
        public int Total { get; set; }
    }

}
