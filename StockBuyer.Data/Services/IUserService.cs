using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockBuyer.Data.Models;

namespace StockBuyer.Data.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByName(string userName);

        Task<User?> GetUserByNameFromCache(string userName);

        Task<Boolean> IsUserValid(string userName, string userToken);

        Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request);
    }
}
