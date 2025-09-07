
namespace SmartBite.Common.NutrientMeasurement.MacroNutrients
{
    public class MacroNutrientProfile
    {
        public Protein Proteins { get; set; }

        public Fat Fats { get; set; }

        public Carbohydrate Carbohydrates { get; set; }

        public MacroNutrientProfile(Protein proteins, Fat fats, Carbohydrate carbohydrates)
        {
            Proteins = proteins ?? throw new ArgumentNullException(nameof(proteins));
            Fats = fats ?? throw new ArgumentNullException(nameof(fats));
            Carbohydrates = carbohydrates ?? throw new ArgumentNullException(nameof(carbohydrates));
        }
    }
}
