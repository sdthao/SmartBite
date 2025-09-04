using SmartBite.Common.Measurement;

namespace SmartBite.Common.CalorieMeasurement
{
    public interface ICalorieMeasurement : IMeasurementScale
    {
        /// <summary>
        /// Converts the calorie measurement to joules.
        /// </summary>
        Calorie ToCalorie();

        /// <summary>
        /// Converts the calorie measurement to kilocalories.
        /// </summary>
        KiloCalorie ToKiloCalorie();

        /// <summary>
        /// Converts the calorie measurement to joules.
        /// </summary>
        Joule ToJoule();

        /// <summary>
        /// Converts the calorie measurement to KiloJoules.
        /// </summary>
        KiloJoule ToKiloJoule();

    }
}
