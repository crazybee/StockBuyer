namespace StockBuyer.Data.Services
{
    public interface IPasswordHasher
    {
        string Generate(string password);
    }
}