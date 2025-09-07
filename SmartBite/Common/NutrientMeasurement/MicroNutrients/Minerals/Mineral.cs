
using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public abstract class Mineral
    {
        /// <summary>
        /// The common name of the mineral.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The measurement of the mineral.
        /// </summary>
        public IWeightMeasurement Measurement { get; }

        protected Mineral(string name, IWeightMeasurement measurement)
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
