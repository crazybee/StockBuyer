using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Models;

namespace StockBuyer.Data.Services
{
    public interface IStocksDataService 
    {
        Task<IEnumerable<StockDto>> GetAllStocks();

        Task<bool> BuyStockById(Guid Id);

        Task<bool> SellStockById(Guid Id);

        Task<StockDto> GetStockById(Guid Id);

    }
}
