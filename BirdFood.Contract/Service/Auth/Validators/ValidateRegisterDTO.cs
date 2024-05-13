using FluentValidation;
using static BirdFood.Contract.Service.Auth.Command;

namespace BirdFood.Contract.Service.Auth.Validators
{
    public class ValidateRegisterDTO : AbstractValidator<Register>
    {
        public ValidateRegisterDTO()
        {
            RuleFor(x => x.RegisterDTO.Username).NotEmpty();
            RuleFor(x => x.RegisterDTO.FullName).NotEmpty();
            RuleFor(x => x.RegisterDTO.Password).NotEmpty();
            RuleFor(x => x.RegisterDTO.ConfirmPassword).NotEmpty();
        }
    }
}
