using Application.CommandResults.Models;
using Application.Commands.Project;
using Application.Handlers.BaseHandler;
using AutoMapper;
using Domain.Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Project
{
    public class DeleteProjectByIdHandler : HandlerBase<DeleteProjectByIdCommand, GenericBaseResult<Boolean>, DeleteProjectByIdHandler>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Domain.Entity.Project> _repository;
        public DeleteProjectByIdHandler(ApplicationDbContext context,  ILogger<DeleteProjectByIdHandler> logger, IMapper mapper, IGenericRepository<Domain.Entity.Project> repository) : base(mapper, logger) => (_context, _mapper,_repository) = (context, mapper,repository);
        protected override async Task<GenericBaseResult<Boolean>> OnHandleRequest(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
        {
            using var txn = await _context.Database.BeginTransactionAsync();
            try
            {

                if (request == null)
                    throw new Exception("Invalid request");

                if (string.IsNullOrEmpty(request.Id))
                    throw new Exception("Project is required");

                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (project == null)
                    throw new Exception("No record found with the Id specified");
                
                project.IsDeleted = true;
                await _repository.UpdateAsync(project);
                await _context.SaveChangesAsync();
                await txn.CommitAsync();

                return new GenericBaseResult<Boolean>(true);
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<Boolean>(false);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }
    }
}


