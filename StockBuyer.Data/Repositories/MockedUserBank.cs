namespace StockBuyer.Data.Repositories
{
    public class MockedUserBank
    {
        private static Dictionary<string, double> moneyDicionary = new()
        {
            { "Liu",  10000.00 },
            { "Hakan",  10000.00 },
            { "Ahmad",  10000.00 },
            { "Laurent",  10000.00 },

        };

        public Dictionary<string, double> MoneyDictionary { get; set; } = moneyDicionary;
    }
}
