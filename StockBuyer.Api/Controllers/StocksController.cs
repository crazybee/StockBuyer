using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockBuyer.Api.Attributes;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : BaseController
    {
        protected StocksController(IUserService userService) : base(userService)
        {
            [CrazybeeAuthorize]
            [HttpGet("allstocks")]
            public async Task<ActionResult<List<StockDto>>?> GetAllStocks()
            {

                var validUser = await IsUserValidInContext(doorRequest.UserName);
                if (validUser == null || validUser.UserType != UserType.Administrator)
                {
                    return this.BadRequest();
                }

                return await this.doorHistoryService.GetDoorHistoryItemsAsync(doorRequest.DoorId);
            }
        }
    }
}
