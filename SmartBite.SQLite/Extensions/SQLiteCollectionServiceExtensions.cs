using SQLite;
using SmartBite.Common;
using SmartBite.Services;
using SmartBite.Models.Food;
using SmartBite.Models.User;
using SmartBite.SQLite.Models;
using SmartBite.SQLite.Mappers;
using SmartBite.SQLite.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBite.SQLite.Extensions
{
    public static class SQLiteCollectionServiceExtensions
    {
        public static IServiceCollection AddSQLite(this IServiceCollection services, string dbPath)
        {
            services.AddSingleton<IMapper<FoodItem, FoodItemEntity>, FoodItemMapper>();
            services.AddSingleton<IMapper<FoodItemEntity, FoodItem>, FoodItemEntityMapper>();
            services.AddSingleton<IMapper<UserProfile, UserProfileEntity>, UserProfileMapper>();
            services.AddSingleton<IMapper<UserProfileEntity, UserProfile>, UserProfileEntityMapper>();

            services.AddSingleton<IFactory<SQLiteAsyncConnection>>(x =>
            {
                var logger = x.GetRequiredService<ILogger<SQLiteFactory>>();
                return new SQLiteFactory(logger, dbPath);
            });

            services.AddSingleton<IUserDbService>(x =>
            {
                var logger = x.GetRequiredService<ILogger<SQLiteUserDbService>>();
                var factory = x.GetRequiredService<IFactory<SQLiteAsyncConnection>>();
                var userProfileMapper = x.GetRequiredService<IMapper<UserProfile, UserProfileEntity>>();
                var userProfileEntityMapper = x.GetRequiredService<IMapper<UserProfileEntity, UserProfile>>();

                var dbResult = factory.Create();
                if (!dbResult.Success || dbResult.Value is null)
                {
                    throw new InvalidOperationException("Failed to create SQLite database connection: " + dbResult.Message);
                }
                
                return new SQLiteUserDbService(logger, userProfileMapper, userProfileEntityMapper, dbResult.Value);
            });

            services.AddSingleton<IFoodDbService>(x =>
            {
                var logger = x.GetRequiredService<ILogger<SQLiteFoodDbService>>();
                var foodItemMapper = x.GetRequiredService<IMapper<FoodItem, FoodItemEntity>>();
                var foodItemEntityMapper = x.GetRequiredService<IMapper<FoodItemEntity, FoodItem>>();

                var factory = x.GetRequiredService<IFactory<SQLiteAsyncConnection>>();
                var dbResult = factory.Create();
                if (!dbResult.Success || dbResult.Value is null)
                {
                    throw new InvalidOperationException("Failed to create SQLite database connection: " + dbResult.Message);
                }

                return new SQLiteFoodDbService(logger, foodItemMapper, foodItemEntityMapper, dbResult.Value);
            });

            return services;
        }
    }
}
