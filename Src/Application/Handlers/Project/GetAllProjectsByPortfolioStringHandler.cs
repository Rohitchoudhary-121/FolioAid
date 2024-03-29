﻿
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
    public class GetAllProjectsByPortfolioStringHandler : HandlerBase<GetAllProjectsByPortfolioStringCommand, GenericBaseResult<PaginatedProjectListResult>, GetAllProjectsByPortfolioStringHandler>
    {
        private readonly IGenericRepository<Domain.Entity.Project> _repository;
        private readonly ApplicationDbContext _context;

        public GetAllProjectsByPortfolioStringHandler(IGenericRepository<Domain.Entity.Project> repository, ApplicationDbContext context, IMapper mapper, ILogger<GetAllProjectsByPortfolioStringHandler> logger) : base(mapper, logger) => (_repository, _context) = (repository, context);

        protected override async Task<GenericBaseResult<PaginatedProjectListResult>> OnHandleRequest(GetAllProjectsByPortfolioStringCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new Exception("Invalid request");

                if (string.IsNullOrEmpty(request.PortfolioString))
                    throw new Exception("Portfolio Id is required");
                //Get User 
                var user = _context.AspNetUsers.FirstOrDefault(u => u.PortfolioString == request.PortfolioString);
                if (user == null)
                    throw new Exception("Invalid Portfolio Id ");

                //Get projects
                var queryable = _repository.GetQueryable().Include(x => x.ProjectIndustryMapping).ThenInclude(x => x.Industry).Include(x => x.ProjectTechnologyMapping).ThenInclude(x => x.Technology).Where(x => x.UserId ==user.Id && x.IsDeleted == false && x.Status== true);

                //Searching
                if (!string.IsNullOrEmpty(request.Search))
                    queryable = queryable.Where(x => x.Title.ToLower().Contains(request.Search.ToLower()));

                //Filter projects by technology
                if (request.Technologies != null && request.Technologies.Any())
                    queryable = queryable.Where(project => project.ProjectTechnologyMapping.Any(mapping => request.Technologies.Contains(mapping.TechnologyId)));

                //Filter projects by industry
                if (request.Industries != null && request.Industries.Any())
                    queryable = queryable.Where(project => project.ProjectIndustryMapping.Any(mapping => request.Industries.Contains(mapping.IndustryId)));

                if (request.PageSize <= 0)
                    request.PageSize = 12;
                var result = new PaginatedProjectListResult();
                result.totalCount = await queryable.CountAsync();

                var projects = await queryable.Skip((request.CurrentPage) * request.PageSize).Take(request.PageSize).ToListAsync();

                result.ProjectList = projects.Select(x => new GetAllProjectsResultModel()
                {
                    UserId = x.UserId,
                    Title = x.Title,
                    Status = x.Status,
                    Summary = x.Summary,
                    Challenges = x.Challenges,
                    Solutions = x.Solutions,
                    BannerImage = x.BannerImage,
                    PublicURL = x.PublicURL,
                    DemoURL = x.DemoURL,
                    AndroidURL = x.AndroidURL,
                    AppleURL = x.AppleURL,
                    Technologies = x.ProjectTechnologyMapping.Select(x => x.Technology.Name).ToArray(),
                    Industries = x.ProjectIndustryMapping.Select(x => x.Industry.Name).ToArray(),
                    CreatedDate = x.CreatedDate,
                     Id = x.Id
                });
                return new GenericBaseResult<PaginatedProjectListResult>(result);
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<PaginatedProjectListResult>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }
    }
}




