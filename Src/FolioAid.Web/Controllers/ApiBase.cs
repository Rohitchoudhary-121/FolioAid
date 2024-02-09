using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FolioAid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBase : ControllerBase
    {
        public ApiBase(IMediator mediator)
            => (Mediator) = (mediator);

        /// <summary>
        /// Mediator to handle request
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Get Response of a Request
        /// </summary>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="request">request</param>
        /// <returns>Response</returns>
        protected virtual async Task<TResult> GetResult<TResult>(IRequest<TResult> request)
            => await Mediator.Send(request);

        /// <summary>
        /// Get Response based on on Result
        /// </summary>
        /// <typeparam name="TResult">BaseResult</typeparam>
        /// <param name="result">result to convert to response</param>
        /// <returns>Response</returns>
        protected ActionResult<TResult> GetResponseFromResult<TResult>(TResult result) where TResult : BaseResult
        {
            return result.ResponseStatusCode switch
            {
                (HttpStatusCode.OK) => Ok(result),
                // To Get the Proble Details 
                (HttpStatusCode.NotFound) => NotFound(result),
                (HttpStatusCode.BadRequest) => BadRequest(result),
                _ => StatusCode((int)result.ResponseStatusCode, result)
            };
        }

        internal string GetVerifyEmailRoute(string id, string token, string portalUrl)
        {
            return $"{portalUrl}/api/Account/verify?id={id}&token={token}";
        }

        internal string GetResetPasswordEmailRoute(string token, string Email, string portalUrl)
        {
            return $"{portalUrl}/account/verifypassword?email={Email}&token={token}";
        }

    }
}
