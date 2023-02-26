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

        private readonly List<ItemEntity> MockedItemList = GenerateMockedItems();
        public Task<IEnumerable<ItemEntity>> GetAllEntities() => Task.FromResult<IEnumerable<ItemEntity>>(this.MockedItemList);

        public Task<ItemEntity?> GetEntityById(Guid id)
        {
            return Task.FromResult(MockedItemList.FirstOrDefault(i => i.Id == id));
        }


        private static List<ItemEntity> GenerateMockedItems()
        {
            var resultsToReturn = new List<ItemEntity>();
            Random rdm = new Random();

            foreach (var item in MockedCompanyList)
            {
                var randomNumber = rdm.Next(100);
                var randomDay = TimeSpan.FromDays(randomNumber);
                resultsToReturn.Add(new ItemEntity()
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
