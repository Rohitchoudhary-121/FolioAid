using Application.CommandResults.Models;
using Application.Commands.Project;
using Application.Handlers.BaseHandler;
using AutoMapper;
using Domain.Common;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Generic;


namespace Application.Handlers.Project
{
    public class GetAllIndustriesHandler : HandlerBase<GetAllIndustriesCommand, GenericBaseResult<IndustryListResult>, GetAllIndustriesHandler>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Industry> _repository;
        public GetAllIndustriesHandler(IGenericRepository<Industry> repository, IMapper mapper, ILogger<GetAllIndustriesHandler> logger) : base(mapper, logger) => (_repository, _mapper) = (repository, mapper);
        protected override async Task<GenericBaseResult<IndustryListResult>> OnHandleRequest(GetAllIndustriesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Get Industries
                var Industries = await _repository.GetQueryable().ToArrayAsync();
                var result = new IndustryListResult();
                result.Industries = Industries.Select(x => new GetAllIndustriesResultModel()
                {
                    Id = x.Id,
                    Name = x.Name
                });
                return new GenericBaseResult<IndustryListResult>(result);
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<IndustryListResult>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }
    }
}


