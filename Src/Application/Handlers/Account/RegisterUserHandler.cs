using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Handlers.BaseHandler;
using Application.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entity.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Application.Handlers.Account
{
    public class RegisterUserHandler : HandlerBase<RegisterUserCommand, GenericBaseResult<ApplicationUserResultModel>, RegisterUserHandler>
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailTemplate _emailTemplate;
        private readonly IHelperService _helperService;
        public RegisterUserHandler(UserManager<ApplicationUser> userManager, IGenericRepository<ApplicationUser> userRepository, ApplicationDbContext context, IMapper mapper, ILogger<RegisterUserHandler> logger, IEmailTemplate emailTemplate, IHelperService helperService) : base(mapper, logger)
            => (_userManager, _userRepository, _context, _mapper, _emailTemplate, _helperService) = (userManager, userRepository, context, mapper, emailTemplate, helperService);
       
        protected override async Task<GenericBaseResult<ApplicationUserResultModel>> OnHandleRequest(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(request.Email).GetAwaiter().GetResult();
                if (user != null && user.Status == true)
                {
                    throw new Exception("Your account is already activated . Please login.");
                }
                else if (user == null || user.Status== false)
                {
                    string randomString = new string(Guid.NewGuid().ToString("N").Where(char.IsLetterOrDigit).Take(10).ToArray());
                    request.CreatedDate = DateTime.UtcNow;
                    request.ModifiedDate = DateTime.UtcNow;
                    request.PortfolioString = randomString;
                    request.ActivationStringExpiryDate = DateTime.UtcNow.AddDays(3);
                    request.ActivationString = new string(Guid.NewGuid().ToString("N").Where(char.IsLetterOrDigit).Take(10).ToArray());
                    var userCreationResult = new IdentityResult();
                    if (user == null)
                    {
                        var newUser = _mapper.Map<ApplicationUser>(request);
                        newUser.UserName = request.Email;
                        userCreationResult = await _userManager.CreateAsync(newUser, request.Password);
                        if (!userCreationResult.Succeeded)
                            throw new Exception(userCreationResult.Errors.First().Description);
                        var roleResult = await _userManager.AddToRoleAsync(newUser, "Admin");
                        if (!roleResult.Succeeded)
                            throw new Exception(userCreationResult.Errors.First().Description);
                    }
                    else
                    {
                        user.FullName = request.FullName;
                        user.ActivationString = request.ActivationString;
                        user.PortfolioString = request.PortfolioString;
                        user.ActivationStringExpiryDate = DateTime.UtcNow.AddDays(3);
                        user.ModifiedDate = DateTime.UtcNow;
                        userCreationResult = await _userManager.UpdateAsync(user);
                        if (!userCreationResult.Succeeded)
                            throw new Exception(userCreationResult.Errors.First().Description);
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, request.Password);
                        if (!resetPasswordResult.Succeeded)
                            throw new Exception(resetPasswordResult.Errors.First().Description);
                    }
                    var encryptedString = request.ActivationString + ',' + request.Email;
                    encryptedString = _helperService.EncryptString(Constant.Key, encryptedString);
                    _emailTemplate.SendActivationLink(request.Email, encryptedString);
                }
                return new GenericBaseResult<ApplicationUserResultModel>(new ApplicationUserResultModel());
                
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<ApplicationUserResultModel>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }
        }

    }
}
