using StockBuyer.Contracts;


namespace StockBuyer.Data.Repositories
{
    public class UserEntityRepository : IUserEntityRepository
    {
       

        private readonly List<UserEntity> MockedUserList = GenerateMockedUsers();
        public Task<IEnumerable<UserEntity>> GetAllUsers() => Task.FromResult<IEnumerable<UserEntity>>(this.MockedUserList);

        public Task<UserEntity?> GetUserByName(string name)
        {
            return Task.FromResult(this.MockedUserList.FirstOrDefault<UserEntity>(u => u.Name == name));
        }

        public Task<UserEntity?> GetUseryById(Guid id)
        {
            return Task.FromResult(this.MockedUserList.FirstOrDefault<UserEntity>(u => u.Id == id));
        }

        private static List<UserEntity> GenerateMockedUsers()
        {
            var resultsToReturn = new List<UserEntity>();
            Random rdm = new Random();

            foreach (var user in Constants.UserList)
            {
                resultsToReturn.Add(new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = user
                });
            }

            return resultsToReturn;
        }
    }
}
