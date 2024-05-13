using DistributedSystem.Contract.DTOs.Shop;
using FluentValidation;
using static DistributedSystem.Contract.Service.Shop.Command;

namespace DistributedSystem.Contract.Service.Shop.Validators
{
    public class ValidatorCreateShopDTO : AbstractValidator<CreateShop>
    {
        public ValidatorCreateShopDTO()
        {
            RuleFor(p => p.createShopDTO.Name).NotEmpty().MinimumLength(4).MaximumLength(30).WithMessage("Name is invalid!");
            RuleFor(x => x.createShopDTO.User_id).NotEmpty();
        }
    }
}
