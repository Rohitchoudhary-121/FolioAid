using Application.CommandResults.Models;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Account
{
    public class ResetPasswordCommand : IRequest<GenericBaseResult<bool>>
    {
        public string Email { get; set; }
        public string newPassword { get; set; }
        public string token { get; set; }
    }
}
