using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.OrderDTO;

namespace BirdFood.Contract.Service.Order
{
    public static class Command
    {
        public record CreateOrder(CreateOrderDTO CreateOrderDTO) : ICommand<BaseResponse> { }
        public record UpdateOrderStatus(Guid Order_id, OrderStatusDTO OrderStatus) : ICommand<BaseResponse> { }
        public record SetNewOrderStatus(InputOrderStatus OrderStatus) : ICommand<BaseResponse> { }
    }
}
