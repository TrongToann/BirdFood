namespace BirdFood.Contract.DTOs.OrderDTO
{
    public interface IOrderDTO
    {
        public Guid Account_id { get; set; }
        public string Payment_Medthod { get; set; }
    }
}
