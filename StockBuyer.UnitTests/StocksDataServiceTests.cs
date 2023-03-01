using Moq;
using StockBuyer.Data.Repositories;
using StockBuyer.Data.Services;

namespace StockBuyer.UnitTests
{
    public class StocksDataServiceTests
    {
        private readonly MockedStockMarket stockMarket= new MockedStockMarket();
        private readonly MockedUserBank userBank = new MockedUserBank();
       
        [Fact]
        public void PasswordHashTest()
        {
            // Arrange
            var expectedHash = "YXNmYXNkbGpkaGZ3b25ma2oVZrYt4isNa1zKr9GfW1CBvGfc";
            var mockedHasher = new PasswordHasher();

            // Execute
            var result = mockedHasher.Generate("123456");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedHash, result);

        }

        [Fact]
        public async void BuyStockByNameTests_UserNotFound_ExpectedError()
        {
            // Arrange
            var expectedError = "No stock with this name found";
            var mockedStockName = "stock1";
           
            var mockedStockEntityRepository = new Mock<IStockEntityRepository>();
            mockedStockEntityRepository.Setup(m => m.GetEntityByName(mockedStockName)).Returns(Task.FromResult<Contracts.StockEntity?>(null));
            
            var mockedDataService = new StocksDataService(mockedStockEntityRepository.Object, this.stockMarket, this.userBank);

            // Execute
            var result = await mockedDataService.BuyStockByName(mockedStockName, It.IsAny<int>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedError, result);

        }

        [Fact]
        public async void BuyStockByNameTests_OverAmount_ExpectedError()
        {
            // Arrange
           
            var mockedStockName = "stock1";
            var expectedError = $"There is not enough amount to buy for this stock {mockedStockName}";
            var mockedAmount = 1000;
            var mockedStockEntity = new Contracts.StockEntity()
            {
                Name = "stock1",
                TotalAmount = 10,
                Id = Guid.NewGuid()
            };
            var mockedStockEntityRepository = new Mock<IStockEntityRepository>();
            mockedStockEntityRepository.Setup(m => m.GetEntityByName(mockedStockName)).Returns(Task.FromResult<Contracts.StockEntity?>(mockedStockEntity));

            var mockedDataService = new StocksDataService(mockedStockEntityRepository.Object, this.stockMarket, this.userBank);
            userBank.MoneyDictionary = new Dictionary<string, double>();
            // Execute
            var result = await mockedDataService.BuyStockByName(mockedStockName, mockedAmount, It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedError, result);

        }

        [Fact]
        public async void BuyStockByNameTests_NotEnoughMoney_ExpectedError()
        {
            // Arrange
            var mockedStockName = "stock1";
            var expectedError = "You don't have enough money";
            var mockedAmount = 1000;
            var mockedUser = "user1";
            this.stockMarket.StockDictionary = new Dictionary<string, StockStatus>()
            {
                 { "stock1",  new StockStatus(){Price = 1000, Amount = 5000 } },
            };
            this.userBank.MoneyDictionary = new Dictionary<string, double>() 
            {
                 { "user1",  100.00 },
            };
            var mockedStockEntity = new Contracts.StockEntity()
            {
                Name = "stock1",
                TotalAmount = 10000,
                Id = Guid.NewGuid()
            };
            var mockedStockEntityRepository = new Mock<IStockEntityRepository>();
            mockedStockEntityRepository.Setup(m => m.GetEntityByName(mockedStockName)).Returns(Task.FromResult<Contracts.StockEntity?>(mockedStockEntity));
            var mockedDataService = new StocksDataService(mockedStockEntityRepository.Object, this.stockMarket, this.userBank);
            // Execute
            var result = await mockedDataService.BuyStockByName(mockedStockName, mockedAmount, mockedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedError, result);

        }

    }
}