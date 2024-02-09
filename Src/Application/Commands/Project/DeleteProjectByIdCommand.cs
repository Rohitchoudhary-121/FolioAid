using Application.CommandResults.Models;
using Application.Handlers.Project;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Project
{
    public class DeleteProjectByIdCommand:GuidModelBase, IRequest<GenericBaseResult<Boolean>>
    {
    }
}
