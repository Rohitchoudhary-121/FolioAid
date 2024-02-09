using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Handlers.BaseHandler;
using AutoMapper;
using Domain.Common;
using Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Handlers.Account
{
    public class LoginUserHandler : HandlerBase<LoginUserCommand, GenericBaseResult<LoginUserResultModel>, LoginUserHandler>
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtIssuerOptions _jwtOptions;

        public LoginUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<LoginUserHandler> logger, JwtIssuerOptions jwtOption) : base(mapper, logger)
           => (_userManager, _mapper, _jwtOptions) = (userManager, mapper, jwtOption);

        protected override async Task<GenericBaseResult<LoginUserResultModel>> OnHandleRequest(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new Exception("Invalid Credentials");
                var result = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!result)
                    throw new Exception("Invalid Credentials");

                if (user != null && user.IsDeleted)
                    throw new Exception("Something went wrong, please contact administrator!");
                if (!user.Status)
                {
                    throw new Exception("Your account is not activated  ,Please activate your account ");
                }

                if (!user.Status &&  user.ActivationStringExpiryDate < DateTime.UtcNow)
                    throw new Exception("Your activate account is expired ,Please sign up again ");


              

                var token = await GetToken(user);
                var resultSuccess = new LoginUserResultModel { Token = token };
                return new GenericBaseResult<LoginUserResultModel>(resultSuccess) { ResponseStatusCode = System.Net.HttpStatusCode.Created, Message = " Login Successfully" };

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<LoginUserResultModel>(null);
                result.AddExceptionLog(ex);
                result.Message = ex.Message;
                return result;
            }

        }

        private async Task<string> GetToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new Dictionary<string, object>
                {
                   {ClaimTypes.Role , userRoles.FirstOrDefault()},
                   {ClaimTypes.Name, user.FullName},
                   {ClaimTypes.Email, user.Email},
                   {ClaimTypes.NameIdentifier, user.Id.ToString()},
                };


            var credentials = new SigningCredentials(new SymmetricSecurityKey(key)
              , SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = string.Join(",", _jwtOptions.Audience),
                NotBefore = _jwtOptions.NotBefore,
                Expires = _jwtOptions.Expiration,
                SigningCredentials = credentials,
                Claims = claims
            };
            var token = jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(tokenDescriptor));
            return token;
        }
    }
}



