using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Models;
using StockBuyer.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBuyer.Data.Services
{
    public class StocksDataService : IStocksDataService
    {
        private readonly IStockEntityRepository _stockEntityRepository;

        public StocksDataService(IStockEntityRepository stockEntityRepository)
        {
            _stockEntityRepository = stockEntityRepository;
        }

        public Task<bool> BuyStockById(Guid Id)
        {
            var foundStock  = this._stockEntityRepository.GetEntityById(Id);
            if (foundStock != null)
            {
                
            }
            return Task.FromResult(false);
        }

        public async Task<IEnumerable<StockDto>> GetAllStocks()
        {
            var stocksToReturn = new List<StockDto>();
            var stocks = await this._stockEntityRepository.GetAllEntities();
            if (stocks.Any())
            {
                foreach (var stock in stocks)
                {
                    stocksToReturn.Add(new StockDto() 
                    {
                        StockId = stock.Id,
                        StockName = stock.Name,
                        StockDescription = stock.Description
                    });
                }
            }
            return stocksToReturn;
        }

        public async Task<StockDto?> GetStockById(Guid Id)
        {
            var stocks = await this.GetAllStocks();
            return stocks.FirstOrDefault(s => s.StockId == Id);
        }

        public Task<bool> SellStockById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
