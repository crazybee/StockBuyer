using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Repositories;

namespace StockBuyer.Data.Services
{
    public class StocksDataService : IStocksDataService
    {
        private readonly IStockEntityRepository _stockEntityRepository;
        private MockedStockMarket stockMarket;
        private MockedUserBank userbank;
        public StocksDataService(IStockEntityRepository stockEntityRepository, MockedStockMarket stockMarket, MockedUserBank userbank)
        {
            _stockEntityRepository = stockEntityRepository;
            this.stockMarket = stockMarket;
            this.userbank = userbank;
        }

        public async Task<string> BuyStockByName(string name, int amount, string userName)
        {
            var foundStock  = await this._stockEntityRepository.GetEntityByName(name);
            var failReason = string.Empty;
            if (foundStock != null)
            {
                if (foundStock.TotalAmount >= amount)
                {
                    var totalMoneyToPay = stockMarket.StockDictionary[name].Price * amount;
                    if (totalMoneyToPay <= this.userbank.MoneyDictionary[userName])
                    {
                        stockMarket.StockDictionary[name].Amount -= amount;
                        userbank.MoneyDictionary[userName] -= totalMoneyToPay;
                        return failReason;
                    }
                    else
                    {
                        failReason = "You don't have enough money";
                    }
                }
                else 
                {
                    failReason = $"There is not enough amount to buy for this stock {name}";        
                }
               
            }
            else 
            {
                failReason = "No stock with this name found";
            }
            return failReason;
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
                        StockDescription = stock.Description,
                        Price = stockMarket.StockDictionary[stock.Name].Price
                        
                    });
                }
            }
            return stocksToReturn;
        }

        public async Task<StockDto> GetStockByName(string name)
        {
            var stockToReturn = new StockDto();
            var foundStock = await this._stockEntityRepository.GetEntityByName(name);
            if (foundStock != null)
            {
                stockToReturn.StockId = foundStock.Id;
                stockToReturn.StockName = foundStock.Name;
                stockToReturn.StockDescription = foundStock.Description;
                stockToReturn.Price = stockMarket.StockDictionary[foundStock.Name].Price;
                stockToReturn.Details = foundStock.CompanyDetails;
            }

            return stockToReturn;
        }

        public async Task<string> SellStockByName(string name, int amount, string userName)
        {
            var failReason = string.Empty;
            var foundStock = await this._stockEntityRepository.GetEntityByName(name);
            if (foundStock != null)
            {
                var currentAmount = stockMarket.StockDictionary[name].Amount;
                var totalMoneyToGet = stockMarket.StockDictionary[name].Price * amount;
                if (amount + currentAmount <= foundStock.TotalAmount)
                {
                    stockMarket.StockDictionary[name].Amount += amount;
                    userbank.MoneyDictionary[userName] += totalMoneyToGet;
                    return failReason;
                }
                else 
                {
                    failReason = "The company doesn't want you do flush their stocks";
                }
            }
            else 
            {
                failReason = "Stock Not found";
            }

            return failReason;
        }
    }
}
