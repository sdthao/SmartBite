namespace SmartBite.Common.CalorieMeasurement
{
    public record Joule(double Value) : ICalorieMeasurement
    {
        public string Name => "Joule";
        public string Symbol => "J";

        public Calorie ToCalorie() => new(Value / 4.184);
        public KiloCalorie ToKiloCalorie() => new(Value / 4184);
        public KiloJoule ToKiloJoule() => new(Value / 1000);
        public Joule ToJoule() => this;

        public static Joule FromJoules(double value) => new(value);
        public static Joule FromCalories(double value) => new(value * 4.184);
        public static Joule FromKiloCalories(double value) => new(value * 4184);
        public static Joule FromKiloJoules(double value) => new(value * 1000);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
