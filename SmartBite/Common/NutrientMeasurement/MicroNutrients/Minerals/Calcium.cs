using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public class Calcium : Mineral
    {
        public Calcium(IWeightMeasurement measurement) 
            : base("Calcium", measurement)
        {
        }
    }
}
