﻿using StockBuyer.Data.Repositories;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Helpers
{
    public static class ServiceDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IStockEntityRepository, StockEntityRepository>();
            services.AddSingleton<IStocksDataService, StocksDataService>();

        }
    }
}