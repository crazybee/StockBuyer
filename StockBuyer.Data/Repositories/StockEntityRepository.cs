using StockBuyer.Contracts;

namespace StockBuyer.Data.Repositories
{
    public class StockEntityRepository : IStockEntityRepository
    {
      

        private readonly List<StockEntity> MockedItemList = GenerateMockedItems();
        public Task<IEnumerable<StockEntity>> GetAllEntities() => Task.FromResult<IEnumerable<StockEntity>>(this.MockedItemList);

        public Task<StockEntity?> GetEntityByName(string name)
        {
            return Task.FromResult(MockedItemList.FirstOrDefault(i => i.Name == name));
        }


        private static List<StockEntity> GenerateMockedItems()
        {
            var resultsToReturn = new List<StockEntity>();
            Random rdm = new Random();

            foreach (var item in Constants.MockedCompanyList)
            {
                var randomNumber = rdm.Next(100);
                var descriptionString = randomNumber %2 == 0 ? "good" : "bad";
                resultsToReturn.Add(new StockEntity()
                {
                    Name = item,
                    Id = Guid.NewGuid(),
                    CurrentPrice = rdm.NextDouble() * randomNumber,
                    Description = $"{item} + is a {descriptionString} company",
                    TotalAmount = 100000,
                    CompanyDetails = $"{item} + is strong for {randomNumber} days",
                });

            }

            return resultsToReturn;
        }

   
    }
}
