namespace SmartBite.Common.WeightMeasurement
{
    public record Ounce(double Value) : IWeightMeasurement
    {
        public string Name => "Ounce";
        public string Symbol => "oz";

        public Gram ToGram() => new(Value * 28.3495);
        public KiloGram ToKiloGram() => new(Value * 0.0283495);
        public Pound ToPound() => new(Value * 0.0625);
        public Ounce ToOunce() => this;

        public static Ounce FromOunces(double value) => new(value);
        public static Ounce FromGrams(double value) => new(value / 28.3495);
        public static Ounce FromKiloGrams(double value) => new(value / 0.0283495);
        public static Ounce FromPounds(double value) => new(value / 0.0625);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
