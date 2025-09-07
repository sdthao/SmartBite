using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MacroNutrients
{
    public class Fat
    {
        public string Name { get; } = "Fat";

        public IWeightMeasurement Measurement { get; }

        public Fat(IWeightMeasurement measurement)
        {
            Measurement = measurement ?? throw new ArgumentNullException(nameof(measurement));
        }

        public static Fat FromGrams(double value)
        {
            return new Fat(new Gram(value));
        }

        public override string ToString()
        {
            return $"{Name}: {Measurement.Value} {Measurement.Symbol}";
        }
    }
}
