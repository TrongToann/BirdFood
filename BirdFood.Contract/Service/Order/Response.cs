using BirdFood.Contract.DTOs.AccountDTO;
using BirdFood.Contract.DTOs.OrderDTO;
using System.Collections.ObjectModel;

namespace BirdFood.Contract.Service.Order
{
    public class Response
    {
        public Guid User_id { get; set; }
        public AccountDTO Account { get; set; }
        public string Payment_Medthod { get; set; }
        public Collection<OrderFoodDTO> OrderProducts { get; set; }
        public OrderShippingDTO OrderShipping { get; set; }
        public Collection<OrderStatusDTO> OrderStatus { get; set; }
    }
}
