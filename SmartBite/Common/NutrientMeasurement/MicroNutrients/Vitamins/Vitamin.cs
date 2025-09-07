using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public abstract class Vitamin
    {
        /// <summary>
        /// The common name of the vitamin (e.g., Vitamin A).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The measurement of the vitamin.
        /// </summary>
        public IWeightMeasurement Measurement { get; }

        protected Vitamin(string name, IWeightMeasurement measurement)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            
            Measurement = measurement;
        }

        public override string ToString()
        {
            return $"{Name}: {Measurement.Value} {Measurement.Symbol}";
        }
    }
}
