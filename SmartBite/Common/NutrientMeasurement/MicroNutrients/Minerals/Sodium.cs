using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public class Sodium : Mineral
    {
        public Sodium(IWeightMeasurement measurement) 
            : base("Sodium", measurement)
        {
        }
    }
}
