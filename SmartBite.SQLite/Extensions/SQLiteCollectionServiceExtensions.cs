using SmartBite.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SmartBite.SQLite.Services;
using SQLite;
using SmartBite.Common;

namespace SmartBite.SQLite.Extensions
{
    public static class SQLiteCollectionServiceExtensions
    {
        public static IServiceCollection AddSQLite(this IServiceCollection services, string dbPath)
        {
            services.AddSingleton<IFactory<SQLiteAsyncConnection>>(x =>
            {
                var logger = x.GetRequiredService<ILogger<SQLiteFactory>>();
                return new SQLiteFactory(logger, dbPath);
            });

            services.AddSingleton<IUserDbService>(x =>
            {
                var logger = x.GetRequiredService<ILogger<SQLiteUserDbService>>();
                var factory = x.GetRequiredService<IFactory<SQLiteAsyncConnection>>();
                var dbResult = factory.Create();
                if (!dbResult.Success || dbResult.Value is null)
                {
                    throw new InvalidOperationException("Failed to create SQLite database connection: " + dbResult.Message);
                }
                return new SQLiteUserDbService(logger, dbResult.Value);
            });

            services.AddSingleton<IFoodDbService>(x =>
            {
                var logger = x.GetRequiredService<ILogger<SQLiteFoodDbService>>();
                var factory = x.GetRequiredService<IFactory<SQLiteAsyncConnection>>();
                var dbResult = factory.Create();
                if (!dbResult.Success || dbResult.Value is null)
                {
                    throw new InvalidOperationException("Failed to create SQLite database connection: " + dbResult.Message);
                }
                return new SQLiteFoodDbService(logger, dbResult.Value);
            });

            return services;
        }
    }
}
