using Newtonsoft.Json;
using SmartBite.Common.CalorieMeasurement;
using SmartBite.Common.Measurement;
using SmartBite.Common.WeightMeasurement;
using SmartBite.Models.Food;
using SQLite;

namespace SmartBite.SQLite.Models
{
    internal class FoodItemEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public double Calories { get; set; }

        public double Weight { get; set; }

        public string WeightScale { get; set; }

        public string NutrientsJson { get; set; }

        public FoodItem ToDomain()
        {
            IWeightMeasurement weightMeasurement = WeightScale switch
            {
                "Gram" => new Gram(Weight),
                "KiloGram" => new KiloGram(Weight),
                "Ounce" => new Ounce(Weight),
                "Pound" => new Pound(Weight),
                _ => throw new InvalidOperationException($"Unknown weight scale: {WeightScale}")
            };

            var nutrients = string.IsNullOrWhiteSpace(NutrientsJson) ? 
                new List<INutrientMeasurement>() : 
                JsonConvert.DeserializeObject<List<INutrientMeasurement>>(NutrientsJson);

            return new FoodItem(
                Name,
                new Calorie(Calories),
                weightMeasurement,
                nutrients?.ToList() ?? new List<INutrientMeasurement>()
            );
        }

        public static FoodItemEntity FromDomain(FoodItem food, string userId)
        {
            return new FoodItemEntity
            {
                UserId = userId,
                Name = food.Name,
                Date = DateTime.Now,
                Calories = food.Calories.Value,
                Weight = food.Weight.Value,
                WeightScale = food.Weight.Name,
                NutrientsJson = JsonConvert.SerializeObject(food.FoodNutrients)
            };
        }

    }
}
