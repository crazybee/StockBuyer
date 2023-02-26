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

        public Task<IEnumerable<StockItem>> GetAllStocks()
        {
            throw new NotImplementedException();
        }

        public Task<StockItem> GetStockById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SellStockById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
