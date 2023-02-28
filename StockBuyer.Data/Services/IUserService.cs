using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Models;

namespace StockBuyer.Data.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByName(string userName);

        Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request);
    }
}
