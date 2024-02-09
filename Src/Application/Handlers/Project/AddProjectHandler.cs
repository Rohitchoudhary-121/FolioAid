using Application.CommandResults.Models;
using Application.Commands.Project;
using Application.Handlers.BaseHandler;
using AutoMapper;
using Domain.Common;
using Domain.Entity;
using Infrastructure;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Project
{
    public class AddProjectHandler : HandlerBase<AddProjectCommand, GenericBaseResult<AddProjectResultModel>, AddProjectHandler>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public AddProjectHandler(ApplicationDbContext context, IMapper mapper, ILogger<AddProjectHandler> logger) : base(mapper, logger) => (_context, _mapper) = (context, mapper);

        protected override async Task<GenericBaseResult<AddProjectResultModel>> OnHandleRequest(AddProjectCommand request, CancellationToken cancellationToken)
        {
            using var txn = await _context.Database.BeginTransactionAsync();
            try
            {
                var projectdata = _mapper.Map<Domain.Entity.Project>(request);
                projectdata.CreatedDate = DateTime.UtcNow;
                projectdata.ModifiedDate = DateTime.UtcNow;

                var prj = await _context.Projects.AddAsync(projectdata);

                if (request.Technologies != null && request.Technologies.Any())
                {
                    var technologyMappings = request.Technologies.Select(x => new ProjectTechnologyMapping { ProjectId = projectdata.Id, TechnologyId = x });
                    await _context.ProjectTechnologyMappings.AddRangeAsync(technologyMappings);
                }

                if (request.Industries != null && request.Industries.Any())
                {
                    var industryMappings = request.Industries.Select(x => new ProjectIndustryMapping { ProjectId = projectdata.Id, IndustryId = x });
                    await _context.ProjectIndustryMappings.AddRangeAsync(industryMappings);
                }

                if (request.ImagesPath != null && request.ImagesPath.Any())
                {
                    var projectImageMappings = request.ImagesPath.Select(x => new ProjectImageGallery { ProjectId = projectdata.Id, ImagePath = x });
                    await _context.ProjectImageGalleries.AddRangeAsync(projectImageMappings);
                }

                if (request.VideosPath != null && request.VideosPath.Any())
                {
                    var projectVideoMappings = request.VideosPath.Select(x => new ProjectVideoGallery { ProjectId = projectdata.Id, VideoPath = x });
                    await _context.ProjectVideoGalleries.AddRangeAsync(projectVideoMappings);
                }
                await _context.SaveChangesAsync();
                await txn.CommitAsync();
                var result = new AddProjectResultModel
                {
                    Title = prj.Entity.Title,
                    Status = prj.Entity.Status,
                    Summary = prj.Entity.Summary,
                    Challenges = prj.Entity.Challenges,
                    Solutions = prj.Entity.Solutions,
                    BannerImage = prj.Entity.BannerImage,
                    PublicURL = prj.Entity.PublicURL,
                    DemoURL = prj.Entity.DemoURL,
                    AndroidURL = prj.Entity.AndroidURL,
                    AppleURL = prj.Entity.AppleURL
                };
                return new GenericBaseResult<AddProjectResultModel>(result);
            }
            catch (Exception ex)
            {
                await txn.RollbackAsync();
                var result = new GenericBaseResult<AddProjectResultModel>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }
    }
}
