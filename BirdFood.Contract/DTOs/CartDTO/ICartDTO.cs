namespace BirdFood.Contract.DTOs.CartDTO
{
    public interface ICartDTO
    {
        public Guid Account_id { get; set; }
        public int Count_Item { get; set; }
    }
}
