using StockBuyer.Data.Helpers;
using System.Security.Cryptography;

namespace StockBuyer.Data.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly string saltString = "asfasdljdhfwonfkjangjkasnggkhkuyvb#%^#$%t9238759y"; // in production this salt is coming from configuration or keyVault 


        public string Generate(string password)
        {
            var salt = saltString.ToByteArray();
            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);


            byte[] hash = pdkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }
    }
}
