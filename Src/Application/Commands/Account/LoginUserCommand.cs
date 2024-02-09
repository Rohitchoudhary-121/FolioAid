using Application.CommandResults.Models;
using Domain.Common;
using MediatR;

namespace Application.Commands.Account
{
    public class LoginUserCommand : GuidModelBase, IRequest<GenericBaseResult<LoginUserResultModel>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}


