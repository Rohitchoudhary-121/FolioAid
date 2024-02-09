using Application.Commands.Account;
using FluentValidation;

namespace Application.Validator
{
    public class LoginUserVailidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserVailidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
        }

    }
}
