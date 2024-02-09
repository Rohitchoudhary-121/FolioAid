using Application.CommandResults.Models;
using Domain.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Commands.Project
{
    public class AddProjectCommand : GuidModelBase, Domain.Entity.EntityHelper.IHasCurrentUserId, IRequest<GenericBaseResult<AddProjectResultModel>>
    {
        [MaxLength(150)]
        [Required]
        public string? Title { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Summary { get; set; }
        public string? Challenges { get; set; }
        public string? Solutions { get; set; }
        public string? BannerImage { get; set; }
        public string? PublicURL { get; set; }
        public string? DemoURL { get; set; }
        public string? AndroidURL { get; set; }
        public string? AppleURL { get; set; }
        public string[]? Technologies { get; set; }
        public string[]? Industries { get; set; }
        public string[]? ImagesPath { get; set; }
        public string[]? VideosPath { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
