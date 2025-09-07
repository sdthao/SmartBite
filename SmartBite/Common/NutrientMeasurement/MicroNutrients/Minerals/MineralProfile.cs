namespace SmartBite.Common.NutrientMeasurement.MicroNutrients.Minerals
{
    public class MineralProfile
    {
        public Calcium Calcium { get; set; }

        public Iodine Iodine { get; set; }

        public Iron Iron { get; set; }

        public Magnesium Magnesium { get; set; }

        public Potassium Potassium { get; set; }

        public Sodium Sodium { get; set; }

        public Zinc Zinc { get; set; }

        public MineralProfile(Calcium calcium, Iodine iodine, Iron iron, Magnesium magnesium, Potassium potassium, Sodium sodium, Zinc zinc)
        {
            Calcium = calcium ?? throw new ArgumentNullException(nameof(calcium));
            Iodine = iodine ?? throw new ArgumentNullException(nameof(iodine));
            Iron = iron ?? throw new ArgumentNullException(nameof(iron));
            Magnesium = magnesium ?? throw new ArgumentNullException(nameof(magnesium));
            Potassium = potassium ?? throw new ArgumentNullException(nameof(potassium));
            Sodium = sodium ?? throw new ArgumentNullException(nameof(sodium));
            Zinc = zinc ?? throw new ArgumentNullException(nameof(zinc));
        }
    }
}
