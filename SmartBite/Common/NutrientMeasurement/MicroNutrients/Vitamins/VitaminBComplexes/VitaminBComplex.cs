using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes
{
    public class VitaminBComplex
    {
        public List<VitaminB> BVitamins { get; private set; }

        public VitaminBComplex(IWeightMeasurement measurement)
        {
            BVitamins = new List<VitaminB>
            {
                new VitaminB1(measurement),
                new VitaminB2(measurement),
                new VitaminB3(measurement),
                new VitaminB5(measurement),
                new VitaminB6(measurement),
                new VitaminB7(measurement),
                new VitaminB9(measurement),
                new VitaminB12(measurement)
            };
        }
    }
}
