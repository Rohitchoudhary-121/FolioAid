using Application.CommandResults.Models;
using Domain.Common;
using Domain.Entity.EntityHelper;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Commands.Project
{
    public class GetAllProjectsCommand : IHasCurrentUserId, IRequest<GenericBaseResult<PaginatedProjectListResult>>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Search { get; set; }
        public string[]? Technologies { get; set; }
        public string[]? Industries { get; set; }

    }
}
