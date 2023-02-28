namespace StockBuyer.Contracts.DTOs
{
    public class StockDto
    {

        public Guid StockId { get; set; }

        public string StockName { get; set;}

        public string StockDescription { get; set;}

    
        public string Details { get; set; }

        public double Price { get; set;}
    }
}