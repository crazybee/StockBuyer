using StockBuyer.Data.Models;

namespace StockBuyer.Data.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByName(string userName);

        Task<Boolean> IsUserValid(string userName, string userToken);

        Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request);
    }
}
