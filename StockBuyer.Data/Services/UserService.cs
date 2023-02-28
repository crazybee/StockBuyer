using StockBuyer.Contracts.DTOs;
using StockBuyer.Data.Helpers;
using StockBuyer.Data.Models;
using StockBuyer.Data.Repositories;

namespace StockBuyer.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserEntityRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private MockedUserBank userbank;
        public UserService(IUserEntityRepository userRepository, IPasswordHasher passwordHasher, MockedUserBank userBank)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.userbank = userBank;
        }

        public async Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }
          
            try
            {
                var user = await this.userRepository.GetUserByName(request.Username);
                var calculatedHash = this.passwordHasher.Generate(request.Password);

                if (user == null || user.PasswordHash != calculatedHash)
                {
                    return null;
                }

                var userToReturn = new User()
                {
                    Name = user.Name,
                    Id = user.Id
                };

                // generate jwt token
                var token = TokenHelper.GenerateJwtToken(userToReturn);

                return new AuthenticationResponse(userToReturn, token);
            }

            catch (Exception ex)
            {

                // TODO:log exception if happens in application insight or other logger like Splunk
                return null;
            }
        }

        public async Task<UserDto?> GetUserByName(string userName)
        {
            var foundUser = await userRepository.GetUserByName(userName);
            double money;
            return foundUser == null ? null : new UserDto() { Name = foundUser.Name, AvailableMoney = userbank.MoneyDictionary.TryGetValue(userName, out money) ?  money : 0.00};
        }

    }
}
