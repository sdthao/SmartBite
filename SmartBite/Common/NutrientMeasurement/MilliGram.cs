namespace SmartBite.Common.NutrientMeasurement
{
    public record MilliGram(double Value) : IVitaminMeasurement
    {
        public string Name => "MilliGrams";

        public string Symbol => "mg";

        public static MilliGram FromMilliGram(double value) => new(value);
        public static MilliGram FromMicroGram(MicroGram microGram) => new(microGram.Value / 1000);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
