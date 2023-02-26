using StockBuyer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockBuyer.Data.Repositories
{
    public class UserEntityRepository : IUserEntityRepository
    {
        private static readonly List<string> UserList = new List<string>()
            {
                "Liu",
                "Hakan",
                "Ahmad",
                "Laurent"
            };

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

            foreach (var user in UserList)
            {
                resultsToReturn.Add(new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = user,
                    TotalCash = 1000
                });
            }

            return resultsToReturn;
        }
    }
}
