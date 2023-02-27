using Microsoft.Extensions.Caching.Memory;
using StockBuyer.Data.Helpers;
using StockBuyer.Data.Models;
using StockBuyer.Data.Repositories;

namespace StockBuyer.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserEntityRepository userRepository;
        private readonly IMemoryCache memoryCache;
        private readonly IPasswordHasher passwordHasher;
        public UserService(IUserEntityRepository userRepository, IMemoryCache memoryCache, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.memoryCache = memoryCache;
            this.passwordHasher = passwordHasher;
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

        public async Task<User?> GetUserByName(string userName)
        {
            var foundUser = await userRepository.GetUserByName(userName);

            return foundUser == null ? null : new User() { Name = foundUser.Name, Id = foundUser.Id};
        }

        public async Task<User?> GetUserByNameFromCache(string userName)
        {
            User? userOutput;
            userOutput = memoryCache.Get<User>(userName);
            if (userOutput == null)
            {
                userOutput = await this.GetUserByName(userName);
                this.memoryCache.Set(userName, userOutput, TimeSpan.FromMinutes(60));
            }

            return userOutput;
        }

        public async Task<bool> IsUserValid(string userName, string password)
        {
            var foundUser = await this.userRepository.GetUserByName(userName);

            if (foundUser != null)
            {
               return foundUser.PasswordHash == this.passwordHasher.Generate(password);
            }

            return false;
        }
    }
}
