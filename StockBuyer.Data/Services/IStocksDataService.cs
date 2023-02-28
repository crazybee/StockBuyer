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

        Task<string> BuyStockByName(string name, int amount, string userName);

        Task<string> SellStockByName(string name, int amount, string userName);

        Task<StockDto> GetStockByName(string name);

    }
}
