using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB5 : VitaminB
    {
        public override string Description { get; } = "Pantothenic Acid";

        public VitaminB5(IWeightMeasurement measurement)
            : base("5", measurement)
        {
        }
    }
}
