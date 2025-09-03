using SmartBite.Common.Measurement;

namespace SmartBite.Models.Food
{
    public class FoodItem
    {
        public string Name { get; set; }

        public ICalorieMeasurement Calories { get; set; }

        public IWeightMeasurement Weight { get; set; }

        public IList<INutrientMeasurement> FoodNutrients { get; set; }

        public FoodItem(string name, ICalorieMeasurement calories, IWeightMeasurement weight, List<INutrientMeasurement> foodNutrients)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Calories = calories ?? throw new ArgumentNullException(nameof(calories));
            Weight = weight ?? throw new ArgumentNullException(nameof(weight));
            FoodNutrients = foodNutrients ?? throw new ArgumentNullException(nameof(foodNutrients));
        }
    }
}
