using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB1 : VitaminB
    {
        public override string Description { get; } = "Thiamine";

        public VitaminB1(IWeightMeasurement measurement)
            : base("1", measurement)
        {
        }
    }
}
