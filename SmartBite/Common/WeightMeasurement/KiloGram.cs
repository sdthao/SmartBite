using SmartBite.Common.Measurement;

namespace SmartBite.Common.WeightMeasurement
{
    public record KiloGram(double Value) : IWeightMeasurement
    {
        public string Name => "KiloGram";
        public string Symbol => "kg";

        public Gram ToGram() => new(Value * 1000);
        public Pound ToPound() => new(Value * 2.20462);
        public Ounce ToOunce() => new(Value * 35.274);
        public KiloGram ToKiloGram() => this;

        public static KiloGram FromKiloGrams(double value) => new(value);
        public static KiloGram FromGrams(double value) => new(value / 1000);
        public static KiloGram FromPounds(double value) => new(value / 2.20462);
        public static KiloGram FromOunces(double value) => new(value / 35.274);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
