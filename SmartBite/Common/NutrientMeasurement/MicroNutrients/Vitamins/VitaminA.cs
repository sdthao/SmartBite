using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public class VitaminA : Vitamin
    {
        public VitaminA(IWeightMeasurement measurement) 
            : base("Vitamin A", measurement)
        {
        }
    }
}
