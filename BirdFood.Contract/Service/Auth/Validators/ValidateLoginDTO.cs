using FluentValidation;
using static BirdFood.Contract.Service.Auth.Command;

namespace BirdFood.Contract.Service.Auth.Validators
{
    public class ValidateLoginDTO : AbstractValidator<Login>
    {
        public ValidateLoginDTO()
        {
            RuleFor(p => p.LoginDTO.UserName).NotEmpty().WithMessage("Invalid Username!");
            RuleFor(p => p.LoginDTO.Password).NotEmpty().WithMessage("Invalide Password");
        }
    }
}
