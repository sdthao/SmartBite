namespace SmartBite.Common.WeightMeasurement
{
    public record MicroGram(double Value) : IWeightMeasurement
    {
        public string Name => "MicroGrams";
        public double Value { get; }
        public string Symbol => "mcg";

        public static MicroGram FromMicroGram(double value) => new(value);
        public static MicroGram FromMilliGram(MilliGram milliGram) => new(milliGram.Value * 1000);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
