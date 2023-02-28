namespace StockBuyer.Api.Controllers
{
    public class StockOperationResponse
    {
        public Guid OperationId { get; set; } = Guid.NewGuid();
        
        public bool IsSuccess { get; set; }

        public string Reason { get; set; }
    }
}