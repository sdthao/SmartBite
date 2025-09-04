using Newtonsoft.Json;
using SmartBite.Common;
using SmartBite.Models.Food;
using SmartBite.SQLite.Models;

namespace SmartBite.SQLite.Mappers
{
    public class FoodItemMapper : IMapper<FoodItem, FoodItemEntity>
    {
        private readonly JsonSerializerSettings _jsonSettings;

        public FoodItemMapper(JsonSerializerSettings jsonSettings)
        {
            _jsonSettings = jsonSettings;
        }

        public IResult<FoodItemEntity> Map(FoodItem value, params object[]? parameters)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (parameters == null || parameters.Length == 0 || parameters[0] is not string userId)
            {
                return Result.Failure<FoodItemEntity>("UserId parameter is required and must be a string.");
            }

            try
            {
                var foodItem = new FoodItemEntity
                {
                    UserId = (string)parameters[0],
                    Name = value.Name,
                    Date = DateTime.Now,
                    Calories = value.Calories.Value,
                    Weight = value.Weight.Value,
                    WeightScale = value.Weight.Name,
                    NutrientsJson = JsonConvert.SerializeObject(value.FoodNutrients, _jsonSettings)
                };

                return Result.Successful(foodItem);
            }
            catch (Exception ex)
            {
                return Result.Failure<FoodItemEntity>($"Mapping failed: {ex.Message}");
            }
        }
    }
}
