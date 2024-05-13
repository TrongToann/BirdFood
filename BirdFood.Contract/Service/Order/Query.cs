using BirdFood.Contract.Abstraction.Message;

namespace BirdFood.Contract.Service.Order
{
    public static class Query
    {
        public record FindOrderById(Guid Order_id) : IQuery<Response>;
    }
}
