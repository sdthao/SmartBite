using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB3 : VitaminB
    {
        public override string Description { get; } = "Niacin";

        public VitaminB3(IWeightMeasurement measurement)
            : base("3", measurement)
        {
        }
    }
}
