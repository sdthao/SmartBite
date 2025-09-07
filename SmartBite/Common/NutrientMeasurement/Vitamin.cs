namespace SmartBite.Common.NutrientMeasurement
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
        public IVitaminMeasurement Measurement { get; }

        protected Vitamin(string name, IVitaminMeasurement measurement)
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
