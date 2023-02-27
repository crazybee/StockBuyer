using StockBuyer.Contracts;

namespace StockBuyer.Data.Repositories
{
    public class StockEntityRepository : IStockEntityRepository
    {
        private static readonly List<string> MockedCompanyList = new List<string>()
            {
                "Apple",
                "Microsoft",
                "Dell",
                "Amazon"
            };

        private readonly List<StockEntity> MockedItemList = GenerateMockedItems();
        public Task<IEnumerable<StockEntity>> GetAllEntities() => Task.FromResult<IEnumerable<StockEntity>>(this.MockedItemList);

        public Task<StockEntity?> GetEntityById(Guid id)
        {
            return Task.FromResult(MockedItemList.FirstOrDefault(i => i.Id == id));
        }


        private static List<StockEntity> GenerateMockedItems()
        {
            var resultsToReturn = new List<StockEntity>();
            Random rdm = new Random();

            foreach (var item in MockedCompanyList)
            {
                var randomNumber = rdm.Next(100);
                var randomDay = TimeSpan.FromDays(randomNumber);
                resultsToReturn.Add(new StockEntity()
                {
                    Name = item,
                    Id  = Guid.NewGuid(),
                    CurrentPrice = rdm.NextDouble()*randomNumber,
                    Description = item + randomNumber.ToString(),
                    TotalAmount = randomNumber,
                    CompanyDetails = $"{item} + is good to buy for {randomDay} days",
                });

            }

            return resultsToReturn;
        }

   
    }
}
