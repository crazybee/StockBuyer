using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockBuyer.Api.Attributes;
using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [CrazybeeAuthorize]
        [HttpGet("getUserByName")]
        public async Task<ActionResult<UserDto>> GetUserByName(string name)
        {
            return this.Ok(await userService.GetUserByName(name));
        }

    }
}
