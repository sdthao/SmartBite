using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public class VitaminK : Vitamin
    {
        public VitaminK(IWeightMeasurement measurement)
        : base("Vitamin K", measurement)
        {
        }
    }
}
