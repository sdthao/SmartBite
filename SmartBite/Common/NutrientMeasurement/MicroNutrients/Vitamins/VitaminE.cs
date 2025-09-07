using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public class VitaminE : Vitamin
    {
        public VitaminE(IWeightMeasurement measurement)
        : base("Vitamin E", measurement)
        {
        }
    }
}
