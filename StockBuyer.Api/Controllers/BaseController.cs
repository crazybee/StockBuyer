using Microsoft.AspNetCore.Mvc;
using StockBuyer.Data.Models;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IUserService userService;

        protected BaseController(IUserService userService)
        {
            this.userService = userService;
        }
        protected async Task<User?> IsUserValidInContext(string userName)
        {
            var user = await this.userService.GetUserByName(userName);
            var userInContext = this.HttpContext.Items["User"] as User;
            if (user == null || userInContext == null || user != userInContext)
            {
                return null;
            }
            return user;
        }
    }
}
