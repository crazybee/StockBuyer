using Microsoft.AspNetCore.Mvc;
using StockBuyer.Api.Attributes;
using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksDataService _stocksDataService;


        public StocksController(IStocksDataService stocksDataService)
        {
            this._stocksDataService = stocksDataService;
        }


        [CrazybeeAuthorize]
        [HttpGet("allstocks")]
        public async Task<ActionResult<IEnumerable<StockDto>>?> GetAllStocks()
        {
            return this.Ok(await _stocksDataService.GetAllStocks());
        }


    }
}
