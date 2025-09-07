using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public class VitaminC : Vitamin
    {
        public VitaminC(IWeightMeasurement measurement) 
            : base("Vitamin C", measurement)
        {
        }
    }
}
