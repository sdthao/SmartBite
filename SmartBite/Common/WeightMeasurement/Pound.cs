using SmartBite.Common.Measurement;

namespace SmartBite.Common.WeightMeasurement
{
    public record Pound(double Value) : IWeightMeasurement
    {
        public string Name => "Pound";
        public string Symbol => "lb";

        public Gram ToGram() => new(Value * 453.592);
        public KiloGram ToKiloGram() => new(Value * 0.453592);
        public Ounce ToOunce() => new(Value * 16);
        public Pound ToPound() => this;

        public static Pound FromPounds(double value) => new(value);
        public static Pound FromGrams(double value) => new(value / 453.592);
        public static Pound FromKiloGrams(double value) => new(value / 0.453592);
        public static Pound FromOunces(double value) => new(value / 16);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
