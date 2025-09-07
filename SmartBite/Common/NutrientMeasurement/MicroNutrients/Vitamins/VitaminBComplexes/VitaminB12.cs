using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminB12 : VitaminB
    {
        public override string Description { get; } = "Pyridoxine";

        public VitaminB12(IWeightMeasurement measurement)
            : base("12", measurement)
        {
        }
    }
}
