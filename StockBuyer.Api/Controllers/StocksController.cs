using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockBuyer.Api.Attributes;
using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : BaseController
    {
        private readonly IStocksDataService _stocksDataService;


        protected StocksController(IUserService userService, IStocksDataService stocksDataService) : base(userService)
        {
            this._stocksDataService = stocksDataService;
        }

        
            [CrazybeeAuthorize]
            [HttpGet("allstocks")]
            public async Task<ActionResult<List<StockDto>>?> GetAllStocks()
            {
                return await this._stocksDataService.GetAllStocks();
            }
        
    }
}
