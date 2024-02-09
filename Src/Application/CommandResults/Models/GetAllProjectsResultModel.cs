using Application.Commands.Project;

namespace Application.CommandResults.Models
{
    public class GetAllProjectsResultModel : AddProjectCommand
    {

    }

    public class PaginatedProjectListResult
    {
        public IEnumerable<GetAllProjectsResultModel>? ProjectList { get; set; }
        public int totalCount { get; set; }

    }
}
