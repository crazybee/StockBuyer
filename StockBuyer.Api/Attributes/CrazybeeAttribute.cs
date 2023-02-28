using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Models;

namespace StockBuyer.Api.Attributes
{
    public class CrazybeeAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserDto;
            if (user == null || string.IsNullOrEmpty(user.Name))
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
