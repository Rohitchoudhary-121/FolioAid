using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Handlers.BaseHandler;
using Application.Handlers.Project;
using Application.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entity;
using Domain.Entity.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Handlers.Account
{
    public class ResetPasswordHandler : HandlerBase<ResetPasswordCommand, GenericBaseResult<bool>, ResetPasswordHandler>
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailTemplate _emailTemplate;
        private readonly ApplicationDbContext _context;
        public ResetPasswordHandler(UserManager<ApplicationUser> userManager, IGenericRepository<ApplicationUser> userRepository, IMapper mapper, ILogger<ResetPasswordHandler> logger, IEmailTemplate emailTemplate) : base(mapper, logger)
           => (_userRepository, _mapper, _emailTemplate, _userManager) = (userRepository, mapper, emailTemplate, userManager);

       
        protected override async Task<GenericBaseResult<bool>> OnHandleRequest(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new Exception("you don't have account with us, Please sign up");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token,  request.newPassword);
                if (!resetPasswordResult.Succeeded)
                    throw new Exception(resetPasswordResult.Errors.First().Description);
                
                return new GenericBaseResult<bool>(true)
                { ResponseStatusCode = System.Net.HttpStatusCode.OK, Message = "Password Changed Successfully" };
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
