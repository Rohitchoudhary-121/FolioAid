using Application.Commands.Project;

namespace Application.CommandResults.Models
{
    public class ProjectDetailedResult : AddProjectCommand
    {
        public string[]? TechnologiesName { get; set; }
        public string[]? IndustriesName { get; set; }
    }
}
