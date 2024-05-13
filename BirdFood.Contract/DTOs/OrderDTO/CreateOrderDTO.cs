

namespace BirdFood.Contract.DTOs.OrderDTO
{
    public class CreateOrderDTO : IOrderDTO
    {
        public Guid Account_id { get; set; }
        public string Payment_Medthod { get; set; }
        public List<InputOrderFood> OrderFoods { get; set; }
        public InputOrderShipping OrderShipping { get; set; }
    }
    public class InputOrderFood
    {
        public Guid Food_id { get; set; }
        public int TotalFood { get; set; }
    }
    public class InputOrderShipping
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
    }
    public class InputOrderStatus
    {
        public Guid Order_id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Date { get; set; }
    }
}
