using StockBuyer.Contracts;

namespace StockBuyer.Data.Repositories
{
    public interface IStockEntityRepository
    {
        Task<IEnumerable<ItemEntity>> GetAllEntities();

        Task<ItemEntity?> GetEntityById(Guid id);
    }
}
