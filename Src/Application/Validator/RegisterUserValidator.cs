using Application.Commands.Account;
using Domain.Entity.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Validator
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator(UserManager<ApplicationUser> _userManager)
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required");
 
        }
    }
}
