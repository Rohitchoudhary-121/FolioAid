using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Handlers.BaseHandler;
using Application.Interface;
using AutoMapper;
using Domain.Common;
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

namespace Application.Handlers.Account
{
    public class SendForgotPasswordEmailHandller : HandlerBase<SendForgotPasswordEmailCommand, GenericBaseResult<bool>, SendForgotPasswordEmailHandller>
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailTemplate _emailTemplate;
        public SendForgotPasswordEmailHandller(UserManager<ApplicationUser> userManager, IGenericRepository<ApplicationUser> userRepository, IMapper mapper, ILogger<SendForgotPasswordEmailHandller> logger, IEmailTemplate emailTemplate) : base(mapper, logger)
           => (_userRepository, _mapper, _emailTemplate, _userManager) = (userRepository, mapper,emailTemplate,userManager);

        protected override async Task<GenericBaseResult<bool>> OnHandleRequest(SendForgotPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null) {
                    throw new Exception("you don't have account with us, Please sign up");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                _emailTemplate.SendResetPasswordLink(user.Email, token);
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
