using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Handlers.BaseHandler;
using Application.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entity.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class ActivationStringHandler : HandlerBase<ActivationStringCommand, GenericBaseResult<bool>, ActivationStringHandler>
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHelperService _helperService;

        public ActivationStringHandler(UserManager<ApplicationUser> userManager, IGenericRepository<ApplicationUser> userRepository, ApplicationDbContext context, IMapper mapper, ILogger<ActivationStringHandler> logger, IHelperService helperService)
          : base(mapper, logger) => (_userManager, _userRepository, _context,_helperService) = (userManager, userRepository, context,helperService);



        protected override async Task<GenericBaseResult<bool>> OnHandleRequest(ActivationStringCommand request, CancellationToken cancellationToken)

        {
            request.ActivationString = _helperService.DecryptString(Constant.Key, request.ActivationString);
            string[] encrypted = request.ActivationString.Split(",");
            request.ActivationString = encrypted[0];
            request.Email = encrypted[1];
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.ActivationString == request.ActivationString && u.Email==request.Email);
            
            try
            {
                if (user == null)
                {
                    throw new Exception("you don't have account with us, Please sign up");
                }
                
                if (user.ActivationStringExpiryDate < DateTime.UtcNow)
                {
                    throw new Exception("That actiation link has expired .You can check your email for a new activation link ," +
                        "sign into your already activated account,or create a new account.");
                }

                user.Status = true;
                await _context.SaveChangesAsync();
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


