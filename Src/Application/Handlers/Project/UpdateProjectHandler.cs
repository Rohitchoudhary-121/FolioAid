using Application.CommandResults.Models;
using Application.Commands.Project;
using Application.Handlers.BaseHandler;
using AutoMapper;
using Domain.Common;
using Domain.Entity;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Generic;

namespace Application.Handlers.Project
{
    public class UpdateProjectHandler : HandlerBase<UpdateProjectCommand, GenericBaseResult<ProjectDetailedResult>, UpdateProjectHandler>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Domain.Entity.Project> _projectRepo;
        public UpdateProjectHandler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateProjectHandler> logger, IGenericRepository<Domain.Entity.Project> projectRepo) : base(mapper, logger) => (_context, _mapper, _projectRepo) = (context, mapper, projectRepo);

        protected override async Task<GenericBaseResult<ProjectDetailedResult>> OnHandleRequest(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            using var txn = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentNullException("Id is required");

                var project = await _context.Projects
                              .Include(x => x.ProjectTechnologyMapping)
                              .Include(x => x.ProjectIndustryMapping).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (project == null)
                    throw new ArgumentNullException("No project found with the id specified");

                project.Title = request.Title;
                project.Status = request.Status;
                project.Summary = request.Summary;
                project.Challenges = request.Challenges;
                project.Solutions = request.Solutions;
                project.BannerImage = request.BannerImage;
                project.PublicURL = request.PublicURL;
                project.DemoURL = request.DemoURL;
                project.AppleURL = request.AppleURL;
                project.AppleURL = request.AppleURL;
                project.ModifiedDate = DateTime.UtcNow;

                await _projectRepo.UpdateAsync(project);

                //Update projectTechnologyMappings
                if (project.ProjectTechnologyMapping != null && project.ProjectTechnologyMapping.Any())
                    _context.ProjectTechnologyMappings.RemoveRange(project.ProjectTechnologyMapping);

                if (request.Technologies != null && request.Technologies.Any())
                {
                    var technologyMappings = request.Technologies.Select(x => new ProjectTechnologyMapping { ProjectId = request.Id, TechnologyId = x });
                    await _context.ProjectTechnologyMappings.AddRangeAsync(technologyMappings);
                }

                //Update projectIndustryMappings
                if (project.ProjectIndustryMapping != null && project.ProjectIndustryMapping.Any())
                    _context.ProjectIndustryMappings.RemoveRange(project.ProjectIndustryMapping);

                if (request.Industries != null && request.Industries.Any())
                {
                    var mapping = request.Industries.Select(x => new ProjectIndustryMapping { ProjectId = request.Id, IndustryId = x });
                    await _context.ProjectIndustryMappings.AddRangeAsync(mapping);
                }

                //Update project image gallery
                var  imageGallery = await _context.ProjectImageGalleries.Where(x => x.ProjectId == request.Id).ToListAsync();
                if (imageGallery != null && imageGallery.Any())
                    _context.ProjectImageGalleries.RemoveRange(imageGallery);

                if (request.ImagesPath != null && request.ImagesPath.Any())
                {
                    var projectImageMappings = request.ImagesPath.Select(x => new ProjectImageGallery { ProjectId = request.Id, ImagePath = x });
                    await _context.ProjectImageGalleries.AddRangeAsync(projectImageMappings);
                }

                //Update project video gallery
                var videoGallery = await _context.ProjectVideoGalleries.Where(x => x.ProjectId == request.Id).ToListAsync();
                if (videoGallery != null && videoGallery.Any())
                    _context.ProjectVideoGalleries.RemoveRange(videoGallery);

                if (request.VideosPath != null && request.VideosPath.Any())
                {
                    var projectVideoMappings = request.VideosPath.Select(x => new ProjectVideoGallery { ProjectId = request.Id, VideoPath = x });
                    await _context.ProjectVideoGalleries.AddRangeAsync(projectVideoMappings);
                }

                await _context.SaveChangesAsync();
                await txn.CommitAsync();
                var result = new ProjectDetailedResult
                {
                    Title = project.Title,
                    Status = project.Status,
                    Summary = project.Summary,
                    Challenges = project.Challenges,
                    Solutions = project.Solutions,
                    BannerImage = project.BannerImage,
                    PublicURL = project.PublicURL,
                    DemoURL = project.DemoURL,
                    AndroidURL = project.AndroidURL,
                    AppleURL = project.AppleURL,
                    Technologies = request.Technologies,
                    Industries = request.Industries,
                    ImagesPath = request.ImagesPath,
                    VideosPath = request.VideosPath
                };
                return new GenericBaseResult<ProjectDetailedResult>(result);
            }
            catch (Exception ex)
            {
                await txn.RollbackAsync();
                var result = new GenericBaseResult<ProjectDetailedResult>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }
    }
}
