using FluentValidation;
using static BirdFood.Contract.Service.Account.Command;

namespace BirdFood.Contract.Service.User.Validators
{
    public class ValidateIUserDTO : AbstractValidator<CreateAccount>
    {
        public ValidateIUserDTO()
        {
            RuleFor(p => p.CreateAccountDTO.Username).NotEmpty().MinimumLength(5).WithMessage("Username is invalid!");
            RuleFor(p => p.CreateAccountDTO.Password).NotEmpty().MinimumLength(4).WithMessage("Password is invalid!");
            RuleFor(p => p.CreateAccountDTO.FullName).NotEmpty().MinimumLength(4).WithMessage("Password is invalid!");
        }

    }
}
