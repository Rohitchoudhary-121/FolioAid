using Application.CommandResults.Models;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Project
{
    public  class CheckPortfolioStringCommand : GuidModelBase, IRequest<GenericBaseResult<bool>>
    {
        public string? PortfolioString  { get; set; }
    }
}
