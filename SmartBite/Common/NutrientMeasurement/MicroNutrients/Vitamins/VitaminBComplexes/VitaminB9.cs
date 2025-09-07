using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB9 : VitaminB
    {
        public override string Description { get; } = "Folate";

        public VitaminB9(IWeightMeasurement measurement)
            : base("9", measurement)
        {
        }
    }
}
