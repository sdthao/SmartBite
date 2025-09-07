using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB2 : VitaminB
    {
        public override string Description { get; } = "Riboflavin";

        public VitaminB2(IWeightMeasurement measurement)
            : base("2", measurement)
        {
        }
    }
}
