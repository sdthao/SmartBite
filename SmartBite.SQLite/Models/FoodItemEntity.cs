using SQLite;

namespace SmartBite.SQLite.Models
{
    public class FoodItemEntity
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
    }
}
