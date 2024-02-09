using Application.CommandResults.Models;
using Application.Commands.Account;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FolioAid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ApiBase
    {
        public AccountController(IMediator mediator) : base(mediator)
        {

        }

        /// <summary>
        ///Register New User
        /// </summary>
        [Route("RegisterUser")]
        [HttpPost]
        public async Task<ActionResult<GenericBaseResult<ApplicationUserResultModel>>> RegisterUser(RegisterUserCommand command)
             => GetResponseFromResult(await Mediator.Send(command));

        /// <summary>
        ///Login Registere User
        /// </summary>
        [Route("LoginUser")]
        [HttpPost]
        public async Task<ActionResult<GenericBaseResult<LoginUserResultModel>>> LoginUser(LoginUserCommand command)
             => GetResponseFromResult(await Mediator.Send(command));

        [Route("ResetPassword")]
        [HttpPut]
        public async Task<ActionResult<GenericBaseResult<bool>>> ResetPassword(ResetPasswordCommand command)
            => GetResponseFromResult(await Mediator.Send(command));


        [Route("SendForgotPasswordEmail")]
        [HttpPost]
        public async Task<ActionResult<GenericBaseResult<bool>>> SendForgotPasswordEmail(SendForgotPasswordEmailCommand command)
           => GetResponseFromResult(await Mediator.Send(command));
        //ActivationString 
        [Route("ActivateAccount")]
        [HttpPost]
        public async Task<ActionResult<GenericBaseResult<bool>>> ActivateAccount(ActivationStringCommand command)
           => GetResponseFromResult(await Mediator.Send(command));

    }
}
