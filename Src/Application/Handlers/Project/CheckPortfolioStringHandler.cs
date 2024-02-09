using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Commands.Project;
using Application.Handlers.BaseHandler;
using Application.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entity.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class CheckPortfolioStringHandler : HandlerBase<CheckPortfolioStringCommand, GenericBaseResult<bool>, CheckPortfolioStringHandler>
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public CheckPortfolioStringHandler(UserManager<ApplicationUser> userManager, IGenericRepository<ApplicationUser> userRepository, ApplicationDbContext context, IMapper mapper, ILogger<CheckPortfolioStringHandler> logger)
          : base(mapper, logger) => (_userManager, _userRepository, _context) = (userManager, userRepository, context);



        protected override async Task<GenericBaseResult<bool>> OnHandleRequest(CheckPortfolioStringCommand request, CancellationToken cancellationToken)

        {


            try
            {
                if (string.IsNullOrEmpty(request.PortfolioString))
                    throw new Exception("Portfolio string  is required");
                var user = _context.AspNetUsers.FirstOrDefault(u => u.PortfolioString == request.PortfolioString);

                if (user== null)
                    throw new Exception("Invalid Portfoliostring");
                    return new GenericBaseResult<bool>(true);

            }

            catch (Exception ex)
            {
                var result = new GenericBaseResult<bool>(false);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }
        }


    }
}


