namespace SmartBite.Common.NutrientMeasurement
{
    public class VitaminA : Vitamin
    {
        public VitaminA(IVitaminMeasurement measurement) 
            : base("Vitamin A", measurement)
        {
        }
    }
}
