﻿using StockBuyer.Contracts;

namespace StockBuyer.Data.Repositories
{
    public interface IStockEntityRepository
    {
        Task<IEnumerable<StockEntity>> GetAllEntities();

        Task<StockEntity?> GetEntityById(Guid id);
    }
}
