using Application.CommandResults.Models;
using Domain.Common;
using Domain.Entity.EntityHelper;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands.Project
{
    public class GetAllTechnologiesCommand : IRequest<GenericBaseResult<TechnologyListResult>>
    {
        
    }
}
