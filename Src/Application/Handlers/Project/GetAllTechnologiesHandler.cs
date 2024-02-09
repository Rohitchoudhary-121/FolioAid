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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Project
{
public class GetAllTechnologiesHandler : HandlerBase<GetAllTechnologiesCommand, GenericBaseResult<TechnologyListResult>, GetAllTechnologiesHandler>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Technology> _repository;
    public GetAllTechnologiesHandler(IGenericRepository<Technology> repository, IMapper mapper, ILogger<GetAllTechnologiesHandler> logger) : base(mapper, logger) => (_repository, _mapper) = (repository, mapper);
    protected override async Task<GenericBaseResult<TechnologyListResult>> OnHandleRequest(GetAllTechnologiesCommand request, CancellationToken cancellationToken)
    {
        try
            {
             //Get technologies
              var technologies = await _repository.GetQueryable().ToArrayAsync();
                var result = new TechnologyListResult();
                result.Technologies =technologies.Select(x => new GetAllTechnologiesResultModel()
                {
                    Id=x.Id,
                    Name=x.Name
                });
                return new GenericBaseResult<TechnologyListResult>(result);
        }
        catch (Exception ex)
        {
            var result = new GenericBaseResult<TechnologyListResult>(null);
            result.AddExceptionLog(ex);
            result.Message = ex.Message;
            return result;
        }

    }
}
}
