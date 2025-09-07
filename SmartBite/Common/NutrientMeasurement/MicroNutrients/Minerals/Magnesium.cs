using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public class Magnesium : Mineral
    {
        public Magnesium(IWeightMeasurement measurement) 
            : base("Magnesium", measurement)
        {
        }
    }
}
