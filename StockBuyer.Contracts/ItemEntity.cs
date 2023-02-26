namespace StockBuyer.Contracts
{
    public class ItemEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double CurrentPrice { get; set; }

        public string Description { get; set; }

        public string CompanyDetails { get; set; }

        public Int64 TotalAmount { get; set;}
    }
}