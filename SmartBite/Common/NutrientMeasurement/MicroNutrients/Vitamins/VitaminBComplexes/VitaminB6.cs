using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB6 : VitaminB
    {
        public override string Description { get; } = "Cyanocobalamin";

        public VitaminB6(IWeightMeasurement measurement)
            : base("6", measurement)
        {
            
        }
    }
}
