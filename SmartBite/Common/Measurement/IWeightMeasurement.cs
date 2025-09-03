using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.Measurement
{
    public interface IWeightMeasurement : IMeasurementScale
    {
        /// <summary>
        /// Converts the weight measurement to grams.
        /// </summary>
        Gram ToGram();

        /// <summary>
        /// Converts the weight measurement to KiloGrams.
        /// </summary>
        KiloGram ToKiloGram();

        /// <summary>
        /// Converts the weight measurement to ounces.
        /// </summary>
        Ounce ToOunce();

        /// <summary>
        /// Converts the weight measurement to pounds.
        /// </summary>
        Pound ToPound();
    }
}
