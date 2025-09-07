using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MacroNutrients
{
    public class Protein
    {
        public string Name { get; } = "Protein";

        public IWeightMeasurement Measurement { get; }

        public Protein(IWeightMeasurement measurement)
        {
            Measurement = measurement ?? throw new ArgumentNullException(nameof(measurement));
        }

        public static Protein FromGrams(double value)
        {
            return new Protein(new Gram(value));
        }

        public override string ToString()
        {
            return $"{Name}: {Measurement.Value} {Measurement.Symbol}";
        }
    }
}
