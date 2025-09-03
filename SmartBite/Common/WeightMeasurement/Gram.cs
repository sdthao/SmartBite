using SmartBite.Common.Measurement;

namespace SmartBite.Common.WeightMeasurement
{
    public record Gram(double Value) : IWeightMeasurement
    {
        public string Name => "Gram";
        public string Symbol => "g";

        public KiloGram ToKiloGram() => new(Value / 1000);
        public Pound ToPound() => new(Value / 453.592);
        public Ounce ToOunce() => new(Value / 28.3495);
        public Gram ToGram() => this;

        public static Gram FromGrams(double value) => new(value);
        public static Gram FromKiloGrams(double value) => new(value * 1000);
        public static Gram FromPounds(double value) => new(value * 453.592);
        public static Gram FromOunces(double value) => new(value * 28.3495);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
