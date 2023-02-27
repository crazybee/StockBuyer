using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockBuyer.Data.Models;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        protected AuthenticationController(IUserService userService) : base(userService)
        {
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticationRequest request)
        {
            var response = await this.userService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
