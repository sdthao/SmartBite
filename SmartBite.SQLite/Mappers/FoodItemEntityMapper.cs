using Newtonsoft.Json;
using SmartBite.Common;
using SmartBite.Models.Food;
using SmartBite.SQLite.Models;
using SmartBite.Common.WeightMeasurement;
using SmartBite.Common.CalorieMeasurement;
using SmartBite.Common.NutrientMeasurement;

namespace SmartBite.SQLite.Mappers
{
    public class FoodItemEntityMapper : IMapper<FoodItemEntity, FoodItem>
    {
        private readonly JsonSerializerSettings _jsonSettings;

        public FoodItemEntityMapper(JsonSerializerSettings jsonSettings)
        {
            _jsonSettings = jsonSettings;
        }

        public IResult<FoodItem> Map(FoodItemEntity value, params object[]? parameter)
        {
            ArgumentNullException.ThrowIfNull(value);

            try
            {
                IWeightMeasurement weightMeasurement = value.WeightScale.ToLower() switch
                {
                    "gram" => new Gram(value.Weight),
                    "kilogram" => new KiloGram(value.Weight),
                    "ounce" => new Ounce(value.Weight),
                    "pound" => new Pound(value.Weight),
                    _ => throw new InvalidOperationException($"Unknown weight scale: {value.WeightScale}")
                };

                var nutrients = string.IsNullOrWhiteSpace(value.NutrientsJson) ?
                    new List<Vitamin>() :
                    JsonConvert.DeserializeObject<List<Vitamin>>(value.NutrientsJson, _jsonSettings);

                var foodItem = new FoodItem(
                    value.Name,
                    new Calorie(value.Calories),
                    weightMeasurement,
                    nutrients?.ToList() ?? new List<Vitamin>()
                );

                return Result.Successful(foodItem);
            }
            catch (Exception ex)
            {
                return Result.Failure<FoodItem>($"Mapping failed: {ex.Message}");
            }
        }
    }
}
