using Domain.Common;
using Domain.Entity.EntityHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FolioAid.Helper
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class HasCurrentUserIdAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                foreach (var arg in context.ActionArguments)
                {
                    if (arg.Value is IHasCurrentUserId hasCurrentUserIdRequest)
                    {
                        var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase));
                        if (userIdClaim == null)
                        {
                            context.Result = new UnauthorizedObjectResult("You are not authorized!");
                            return;
                        }

                        hasCurrentUserIdRequest.UserId = userIdClaim.Value;
                    }
                }

                await next.Invoke();
            }
            catch (Exception ex)
            {
                context.Result = new ObjectResult("An error occurred: " + ex.Message)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
