using StockBuyer.Data.Repositories;
using StockBuyer.Data.Services;

namespace StockBuyer.Api.Helpers
{
    public static class ServiceDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IUserEntityRepository, UserEntityRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IStockEntityRepository, StockEntityRepository>();
            services.AddSingleton<IStocksDataService, StocksDataService>();
        }
    }
}
