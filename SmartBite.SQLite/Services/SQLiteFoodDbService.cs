using SQLite;
using SmartBite.Common;
using SmartBite.Services;
using SmartBite.Models.Food;
using SmartBite.SQLite.Models;
using Microsoft.Extensions.Logging;

namespace SmartBite.SQLite.Services
{
    public class SQLiteFoodDbService : IFoodDbService
    {
        private readonly ILogger<SQLiteFoodDbService> _logger;

        private readonly IMapper<FoodItem, FoodItemEntity> _foodItemMapper;

        private readonly IMapper<FoodItemEntity, FoodItem> _foodItemEntityMapper;

        private readonly SQLiteAsyncConnection _db;

        public SQLiteFoodDbService(ILogger<SQLiteFoodDbService> logger,
            IMapper<FoodItem, FoodItemEntity> foodItemMapper,
            IMapper<FoodItemEntity, FoodItem> foodItemEntityMapper,
            SQLiteAsyncConnection db)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _foodItemMapper = foodItemMapper ?? throw new ArgumentNullException(nameof(foodItemMapper));
            
            _foodItemEntityMapper = foodItemEntityMapper ?? throw new ArgumentNullException(nameof(foodItemEntityMapper));

            _db = db ?? throw new ArgumentNullException(nameof(db));
            _db.CreateTableAsync<FoodItemEntity>();
        }

        public async Task<IResult<List<FoodItem>>> GetFoodsForUserAsync(string userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId);

            try
            {
                _logger.LogInformation($"Retrieving food items for userId: {userId} on date: {date.Date}");

                var allUserFood = await _db.Table<FoodItemEntity>()
                                     .Where(f => f.UserId == userId)
                                     .ToListAsync();

                var items = allUserFood.Where(f => f.Date.Date == date.Date).ToList();

                // TODO: Need to apply SqliteFunction.Date()
                //var items = await _db.Table<FoodItemEntity>()
                //                     .Where(f => f.UserId == userId && f.Date == date.Date)
                //                     .ToListAsync();

                var foodItems = items
                .Select(e => _foodItemEntityMapper.Map(e))
                .Where(map => map.Success)
                .Select(map => map.Value)
                .ToList();

                return Result.Successful(foodItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Result.Failure<List<FoodItem>>(ex.Message);
            }
        }

        public async Task<IResult> AddFoodItemAsync(string userId, FoodItem food)
        {
            ArgumentNullException.ThrowIfNull(userId);

            ArgumentNullException.ThrowIfNull(food);
            
            try
            {
                _logger.LogInformation($"Adding food item '{food.Name}' for userId: {userId}");

                var map = _foodItemMapper.Map(food, userId);

                if (map.Success == false)
                {
                    _logger.LogError(map.Message);

                    return Result.Failure(map.Message);
                }

                var entity = map.Value;

                await _db.InsertAsync(entity);
                
                return Result.Successful();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Result.Failure(ex.Message);
            }
        }

        public async Task<IResult> RemoveFoodItemAsync(string userId, string foodName)
        {
            ArgumentNullException.ThrowIfNull(userId);

            ArgumentNullException.ThrowIfNull(foodName);
            
            try
            {
                _logger.LogInformation($"Removing food item '{foodName}' for userId: {userId}");

                var entity = await _db.Table<FoodItemEntity>()
                                      .Where(f => f.UserId == userId && f.Name == foodName)
                                      .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning($"Food item '{foodName}' for userId '{userId}' not found.");

                    return Result.Failure("Item not found.");
                }

                await _db.DeleteAsync(entity);

                return Result.Successful();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Result.Failure(ex.Message);
            }
        }

        public async Task<IResult> ClearFoodsAsync(string userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            try
            {
                _logger.LogInformation($"Clearing all food items for userId: {userId}");

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
                _logger.LogError(ex.Message);

                return Result.Failure(ex.Message);
            }
        }
    }
}
