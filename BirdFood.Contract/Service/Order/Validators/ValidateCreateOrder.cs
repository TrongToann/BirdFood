using DistributedSystem.Domain.Entitites;
using FluentValidation;
using static DistributedSystem.Contract.Service.Order.Command;

namespace DistributedSystem.Contract.Service.Order.Validators
{
    public class ValidateCreateOrder : AbstractValidator<CreateOrder>
    {
        public ValidateCreateOrder()
        {
            RuleFor(x => x.CreateOrderDTO.User_id).NotEmpty();
            RuleFor(x => x.CreateOrderDTO.Payment_Medthod).NotEmpty();
            RuleFor(x => x.CreateOrderDTO.OrderProducts)
                .Must(orderProducts => orderProducts.All(op => !string.IsNullOrEmpty(op.Product_id.ToString())))
                .Must(orderProducts => orderProducts.All(op => op.TotalProduct > 0))
                .WithMessage("TotalProduct in all OrderProducts must not be empty!");

            RuleFor(x => x.CreateOrderDTO.OrderShipping)
                .Must(orderShipping => !string.IsNullOrEmpty(orderShipping.Province))
                .Must(orderShipping => !string.IsNullOrEmpty(orderShipping.District))
                .Must(orderShipping => !string.IsNullOrEmpty(orderShipping.Address))
                .WithMessage("Order Shipping must not be empty!");



        }
    }
}
