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
            var user = this.HttpContext.Items["User"] as UserDto;
            Console.WriteLine(user.Name);
            return this.Ok(await _stocksDataService.GetAllStocks());
        }

        [CrazybeeAuthorize]
        [HttpGet("getStockByName")]
        public async Task<ActionResult<StockDto>?> GetStock(string name)
        {
            return this.Ok(await _stocksDataService.GetStockByName(name));
        }

        [CrazybeeAuthorize]
        [HttpGet("buyStockById")]
        public async Task<ActionResult<StockOperationResponse>> BuyStockById(string name, int amount)
        {
            var user = this.HttpContext.Items["User"] as UserDto;

            if (user != null)
            {
                var result = await _stocksDataService.BuyStockByName(name, amount, user.Name);
                return this.Ok(new StockOperationResponse()
                {
                    IsSuccess = result == string.Empty,
                    Reason = result,
                    OperationId = Guid.NewGuid()
                });
            }
           
            return this.NotFound();
        }

        [CrazybeeAuthorize]
        [HttpPost("sellStockByName")]
        public async Task<ActionResult<StockOperationResponse>?> SellStock(string name, int amount)
        {
            var user = this.HttpContext.Items["User"] as UserDto;
            if (user != null)
            {
                var result = await _stocksDataService.SellStockByName(name, amount, user.Name);
                return this.Ok(new StockOperationResponse()
                {
                    IsSuccess = result == string.Empty,
                    Reason = result,
                    OperationId = Guid.NewGuid()
                });
            }
          
            return this.NotFound();
        }


    }
}
