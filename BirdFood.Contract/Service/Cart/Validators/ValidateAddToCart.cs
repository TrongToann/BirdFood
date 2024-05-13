using FluentValidation;
using static BirdFood.Contract.Service.Cart.Command;

namespace BirdFood.Contract.Service.Cart.Validators
{
    public class ValidateAddToCart : AbstractValidator<AddToCart>
    {
        public ValidateAddToCart() 
        {
            RuleFor(x => x.addToCart.Account_id).NotEmpty();
            RuleFor(x => x.addToCart.InputFoodDTO.Food_id).NotEmpty();
            RuleFor(x => x.addToCart.InputFoodDTO.Total).NotEmpty();
        }
    }
}
