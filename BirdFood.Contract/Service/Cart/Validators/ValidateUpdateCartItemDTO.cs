using FluentValidation;
using static BirdFood.Contract.Service.Cart.Command;

namespace BirdFood.Contract.Service.Cart.Validators
{
    public class ValidateUpdateCartItemDTO : AbstractValidator<UpdateCartItem>
    {
        public ValidateUpdateCartItemDTO()
        {
            RuleFor(x => x.Cart_id).NotEmpty();
            RuleFor(x => x.UpdateCartItemDTO.Food_id).NotEmpty();
            RuleFor(x => x.UpdateCartItemDTO.Type).NotEmpty();
            RuleFor(x => x.UpdateCartItemDTO.Quantity).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
