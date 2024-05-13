using DistributedSystem.Contract.DTOs.Shop;
using FluentValidation;

namespace DistributedSystem.Contract.Service.Shop.Validators
{
    public class ValidateIShopDTO : AbstractValidator<IShopDTO>
    {
        public ValidateIShopDTO()
        {
            RuleFor(p => p.Name).NotEmpty().MinimumLength(4).MaximumLength(30).WithMessage("Name is invalid!");
            RuleFor(p => p.Avatar).NotEmpty().WithMessage("Avatar is invalid!");
            RuleFor(p => p.Description).NotEmpty().MinimumLength(4).WithMessage("Description is invalid!");
        }
    }
}
