namespace SmartBite.Common.CalorieMeasurement
{
    public record Calorie(double Value) : ICalorieMeasurement
    {
        public string Name => "Calorie";
        public string Symbol => "cal";

        public KiloCalorie ToKiloCalorie() => new(Value / 1000);
        public Joule ToJoule() => new(Value * 4.184);
        public KiloJoule ToKiloJoule() => new((Value * 4.184) / 1000);
        public Calorie ToCalorie() => this;

        public static Calorie FromCalories(double value) => new(value);
        public static Calorie FromKiloCalories(double value) => new(value * 1000);
        public static Calorie FromJoules(double value) => new(value / 4.184);
        public static Calorie FromKiloJoules(double value) => new((value * 1000) / 4.184);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
