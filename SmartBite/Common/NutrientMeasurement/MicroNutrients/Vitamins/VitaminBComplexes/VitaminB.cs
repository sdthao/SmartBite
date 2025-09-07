using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public abstract class VitaminB : Vitamin
    {
        public abstract string Description { get; }

        public VitaminB(string name, IWeightMeasurement measurement)
        : base("Vitamin B" + name, measurement)
        {
        }
    }
}
