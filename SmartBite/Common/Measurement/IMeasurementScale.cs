
namespace SmartBite.Common.Measurement
{
    public interface IMeasurementScale
    {
        /// <summary>
        /// The name of the measurement scale.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The numeric value of the measurement.
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Symbol representing the measurement unit.
        /// </summary>
        string Symbol { get; }
    }
}
