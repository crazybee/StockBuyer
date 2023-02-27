namespace StockBuyer.Contracts
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Int64 TotalCash { get; set; }

        public string PasswordHash { get; } = "YXNmYXNkbGpkaGZ3b25ma/Es/E2NT0tGev51L2CWhXCsKCrc"; // this is just to simplify it, the data should be coming from db in reality
    }
}
