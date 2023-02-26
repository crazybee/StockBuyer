using StockBuyer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBuyer.Data.Repositories
{
    public interface IUserEntityRepository
    {
        Task<UserEntity?> GetUseryById(Guid id);

        Task<UserEntity?> GetUserByName(string name);

        Task<IEnumerable<UserEntity>> GetAllUsers();

    }
}
