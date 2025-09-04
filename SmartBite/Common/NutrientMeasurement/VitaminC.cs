namespace SmartBite.Common.NutrientMeasurement
{
    public class VitaminC : Vitamin
    {
        public VitaminC(IVitaminMeasurement measurement) 
            : base("Vitamin C", measurement)
        {
        }
    }
}
