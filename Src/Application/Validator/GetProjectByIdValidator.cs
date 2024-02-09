using Application.Commands.Project;
using FluentValidation;

namespace Application.Validator
{
    public class GetProjectByIdValidator : AbstractValidator<GetProjectByIdCommand>
    {
        public GetProjectByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Project Id is Required");

        }
    }
}
