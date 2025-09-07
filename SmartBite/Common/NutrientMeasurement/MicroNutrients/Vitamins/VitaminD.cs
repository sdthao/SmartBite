using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public class VitaminD : Vitamin
    {
        public VitaminD(IWeightMeasurement measurement) 
            : base("Vitamin D", measurement)
        {
        }
    }
}
