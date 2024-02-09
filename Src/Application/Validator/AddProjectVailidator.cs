using Application.Commands.Project;
using FluentValidation;

namespace Application.Validator
{
    public class AddProjectVailidator : AbstractValidator<AddProjectCommand>
    {
        public AddProjectVailidator()
        {
            RuleFor(x => x.BannerImage).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
