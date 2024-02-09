using Application.CommandResults.Models;
using Domain.Common;
using MediatR;

namespace Application.Commands.Project
{
    public class GetProjectByIdCommand : GuidModelBase, IRequest<GenericBaseResult<ProjectDetailedResult>>
    {

    }
}
