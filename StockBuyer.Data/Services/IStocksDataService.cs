using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockBuyer.Data.Models;

namespace StockBuyer.Data.Services
{
    public interface IStocksDataService 
    {
        Task<IEnumerable<StockItem>> GetAllStocks();

        Task<bool> BuyStockById(Guid Id);

        Task<bool> SellStockById(Guid Id);

        Task<StockItem> GetStockById(Guid Id);

    }
}
