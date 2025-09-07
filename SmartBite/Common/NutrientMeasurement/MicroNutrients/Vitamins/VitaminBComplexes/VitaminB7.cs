using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB7 : VitaminB
    {
        public override string Description { get; } = "Biotin";

        public VitaminB7(IWeightMeasurement measurement)
            : base("7", measurement)
        {
        }
    }
}
