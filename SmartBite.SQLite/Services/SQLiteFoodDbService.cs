using Microsoft.Extensions.Logging;
using SmartBite.Common;
using SmartBite.Models.Food;
using SmartBite.Services;
using SmartBite.SQLite.Models;
using SQLite;

namespace SmartBite.SQLite.Services
{
    public class SQLiteFoodDbService : IFoodDbService
    {
        private readonly ILogger<SQLiteFoodDbService> _logger;

        private readonly SQLiteAsyncConnection _db;

        public SQLiteFoodDbService(ILogger<SQLiteFoodDbService> logger,  SQLiteAsyncConnection db)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _db = db ?? throw new ArgumentNullException(nameof(db));
            _db.CreateTableAsync<FoodItemEntity>();
        }

        public async Task<IResult<List<FoodItem>>> GetFoodsForUserAsync(string userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId);

            try
            {
                var items = await _db.Table<FoodItemEntity>()
                                     .Where(f => f.UserId == userId && f.Date == date.Date)
                                     .ToListAsync();

                var foodItems = items.Select(e => e.ToDomain()).ToList();

                return Result.Successful(foodItems);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<FoodItem>>(ex.Message);
            }
        }

        public async Task<IResult> AddFoodItemAsync(string userId, FoodItem food)
        {
            ArgumentNullException.ThrowIfNull(userId);

            ArgumentNullException.ThrowIfNull(food);
            
            try
            {
                var entity = FoodItemEntity.FromDomain(food, userId);

                await _db.InsertAsync(entity);
                
                return Result.Successful();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<IResult> RemoveFoodItemAsync(string userId, string foodName)
        {
            ArgumentNullException.ThrowIfNull(userId);

            ArgumentNullException.ThrowIfNull(foodName);
            
            try
            {
                var entity = await _db.Table<FoodItemEntity>()
                                      .Where(f => f.UserId == userId && f.Name == foodName)
                                      .FirstOrDefaultAsync();

                if (entity == null)
                    return Result.Failure("Item not found.");

                await _db.DeleteAsync(entity);

                return Result.Successful();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<IResult> ClearFoodsAsync(string userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            try
            {
                var items = await _db.Table<FoodItemEntity>()
                                     .Where(f => f.UserId == userId)
                                     .ToListAsync();

                foreach (var item in items)
                {
                    await _db.DeleteAsync(item);
                }

                return Result.Successful();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
