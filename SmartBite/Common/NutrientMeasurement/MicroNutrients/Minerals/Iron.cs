using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public class Iron : Mineral
    {
        public Iron(IWeightMeasurement measurement) 
            : base("Iron", measurement)
        {
        }
    }
}
