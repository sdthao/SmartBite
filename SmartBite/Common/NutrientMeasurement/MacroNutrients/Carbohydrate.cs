using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MacroNutrients
{
    public class Carbohydrate
    {
        public string Name { get; } = "Carbohydrate";

        public IWeightMeasurement Measurement { get; }

        public Carbohydrate(IWeightMeasurement measurement)
        {
            Measurement = measurement ?? throw new ArgumentNullException(nameof(measurement));
        }

        public static Carbohydrate FromGrams(double value)
        {
            return new Carbohydrate(new Gram(value));
        }

        public override string ToString()
        {
            return $"{Name}: {Measurement.Value} {Measurement.Symbol}";
        }
    }
}
