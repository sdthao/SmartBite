using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public class Zinc : Mineral
    {
        public Zinc(IWeightMeasurement measurement) 
            : base("Zinc", measurement)
        {
        }
    }
}
