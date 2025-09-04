namespace SmartBite.Common.NutrientMeasurement
{
    public class VitaminD : Vitamin
    {
        public VitaminD(IVitaminMeasurement measurement) 
            : base("Vitamin D", measurement)
        {
        }
    }
}
