using SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins.VitaminBComplexes;

namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Vitamins
{
    public class VitaminProfile
    {
        public VitaminA VitaminA { get; set; }

        public VitaminBComplex VitaminBComplexes { get; set; }

        public VitaminC VitaminC { get; set; }

        public VitaminD VitaminD { get; set; }

        public VitaminE VitaminE { get; set; }

        public VitaminK VitaminK { get; set; }

        public VitaminProfile(VitaminA vitaminA, VitaminBComplex vitaminBComplexes, VitaminC vitaminC, VitaminD vitaminD, VitaminE vitaminE, VitaminK vitaminK)
        {
            VitaminA = vitaminA ?? throw new ArgumentNullException(nameof(vitaminA));
            VitaminBComplexes = vitaminBComplexes ?? throw new ArgumentNullException(nameof(vitaminBComplexes));
            VitaminC = vitaminC ?? throw new ArgumentNullException(nameof(vitaminC));
            VitaminD = vitaminD ?? throw new ArgumentNullException(nameof(vitaminD));
            VitaminE = vitaminE ?? throw new ArgumentNullException(nameof(vitaminE));
            VitaminK = vitaminK ?? throw new ArgumentNullException(nameof(vitaminK));
        }
    }
}
