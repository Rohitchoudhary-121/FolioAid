using Application.CommandResults.Models;
using Application.Commands.Project;
using Application.Handlers.BaseHandler;
using AutoMapper;
using Domain.Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Project
{
    public class GetProjectByIdHandler : HandlerBase<GetProjectByIdCommand, GenericBaseResult<ProjectDetailedResult>, GetProjectByIdHandler>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public GetProjectByIdHandler(ApplicationDbContext context, IMapper mapper, ILogger<GetProjectByIdHandler> logger) : base(mapper, logger) => (_context, _mapper) = (context, mapper);
        protected override async Task<GenericBaseResult<ProjectDetailedResult>> OnHandleRequest(GetProjectByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _context.Projects.Include(x => x.ProjectTechnologyMapping).ThenInclude(x => x.Technology).Include(x => x.ProjectIndustryMapping).ThenInclude(x => x.Industry).FirstOrDefaultAsync(x => x.Id == request.Id);
                if (project == null || project.IsDeleted)
                    throw new Exception("No record found with the Id specified");

                var projectImages = await _context.ProjectImageGalleries.Where(x => x.ProjectId == request.Id).Select(x => x.ImagePath).ToArrayAsync();
                var projectVideos = await _context.ProjectVideoGalleries.Where(x => x.ProjectId == request.Id).Select(x => x.VideoPath).ToArrayAsync();

                var result = _mapper.Map<ProjectDetailedResult>(project);
                result.Industries = project.ProjectIndustryMapping.Select(x => x.IndustryId).ToArray();
                result.Technologies = project.ProjectTechnologyMapping.Select(x => x.TechnologyId).ToArray();
                result.ImagesPath = projectImages;
                result.VideosPath = projectVideos;
                result.TechnologiesName = project.ProjectTechnologyMapping.Select(x => x.Technology.Name).ToArray();
                result.IndustriesName = project.ProjectIndustryMapping.Select(x => x.Industry.Name).ToArray();

                return new GenericBaseResult<ProjectDetailedResult>(result);
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<ProjectDetailedResult>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }
    }
}
