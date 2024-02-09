using Application.CommandResults.Models;
using Application.Commands.Project;
using Domain.Common;
using FolioAid.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FolioAid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProjectController : ApiBase
    {
        public ProjectController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        ///Add New Project
        /// </summary>
        [HttpPost("AddProject")]
        [HasCurrentUserId]
        [Authorize]
        public async Task<ActionResult<GenericBaseResult<AddProjectResultModel>>> AddProject([FromBody] AddProjectCommand command)
            => GetResponseFromResult(await Mediator.Send(command));

        /// <summary>
        ///Update Project
        /// </summary>
        [HttpPost("UpdateProject")]
        [HasCurrentUserId]
        [Authorize]
        public async Task<ActionResult<GenericBaseResult<ProjectDetailedResult>>> UpdateProject([FromBody] UpdateProjectCommand command)
            => GetResponseFromResult(await Mediator.Send(command));

        /// <summary>
        ///Get Project by Id
        /// </summary>
        [HttpPost("GetProjectById")]
        public async Task<ActionResult<GenericBaseResult<ProjectDetailedResult>>> GetProjectById([FromBody] GetProjectByIdCommand command)
        {
            return GetResponseFromResult(await Mediator.Send(command));
        }

        /// <summary>
        ///Get All Projects
        /// </summary>
        [HttpPost("GetAllProjects")]
        [HasCurrentUserId]
        [Authorize]
        public async Task<ActionResult<GenericBaseResult<PaginatedProjectListResult>>> GetAllProjects([FromBody] GetAllProjectsCommand command)
        {
            return GetResponseFromResult(await Mediator.Send(command));
        }
        /// <summary>
        ///Check  PotfolioString  is Valid Or Not 
        /// </summary>

        [HttpPost("CheckPortolioString")]
        public async Task<ActionResult<GenericBaseResult<bool>>> CheckPortfolioString([FromBody] CheckPortfolioStringCommand command)
        {
            return GetResponseFromResult(await Mediator.Send(command));
        }
        /// <summary>
        ///Get All Projects  BY PotfolioString 
        /// </summary>

        [HttpPost("GetAllProjectsByPortolioString")]        
        public async Task<ActionResult<GenericBaseResult<PaginatedProjectListResult>>> GetAllProjectsByPortfolioString([FromBody] GetAllProjectsByPortfolioStringCommand command)
        {
            return GetResponseFromResult(await Mediator.Send(command));
        }

        /// <summary>
        ///Get All Technologies
        /// </summary>
        [HttpGet("GetAllTechnologies")]
    
        public async Task<ActionResult<GenericBaseResult<TechnologyListResult>>> GetAllTechnologies()
        {
            return GetResponseFromResult(await Mediator.Send(new GetAllTechnologiesCommand()));
        }

        /// <summary>
        ///Get All Industries
        /// </summary>
        [HttpGet("GetAllIndustries")]
       
        public async Task<ActionResult<GenericBaseResult<IndustryListResult>>> GetAllIndustries()
        {
            return GetResponseFromResult(await Mediator.Send(new GetAllIndustriesCommand()));
        }

        /// <summary>
        ///Delete Project
        /// </summary>
        [HttpPost("DeleteProject")]
        [HasCurrentUserId]
        [Authorize]
        public async Task<ActionResult<GenericBaseResult<Boolean>>> DeleteProject([FromBody] DeleteProjectByIdCommand command)
            => GetResponseFromResult(await Mediator.Send(command));

    }
}
