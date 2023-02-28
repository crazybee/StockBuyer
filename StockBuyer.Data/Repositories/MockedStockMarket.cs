namespace StockBuyer.Data.Repositories
{
    public class MockedStockMarket
    {
        private readonly Dictionary<string, StockStatus> stockDicionary = new Dictionary<string, StockStatus>()
        {
            { "Apple",  new StockStatus(){Price = 100.78, Amount = 5000 } },
            { "Amazon",  new StockStatus(){Price = 30.67, Amount = 5000 } },
            { "Microsoft",  new StockStatus(){Price = 200.41, Amount = 15000 } },
            { "Dell",   new StockStatus(){Price = 15.60, Amount = 15000 } }

        };

        public Dictionary<string, StockStatus> StockDictionary => stockDicionary;
    }
}
