namespace StockBuyer.Data.Models
{
    public class AuthenticationResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string Token { get; set; }
        public AuthenticationResponse(User user, string token)
        {
            UserId = user.Id;

            UserName = user.Name;

            Token = token;

        }
    }
}