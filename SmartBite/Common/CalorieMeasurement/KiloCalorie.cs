namespace SmartBite.Common.CalorieMeasurement
{
    public record KiloCalorie(double Value) : ICalorieMeasurement
    {
        public string Name => "KiloCalorie";
        public string Symbol => "kcal";

        public Calorie ToCalorie() => new(Value * 1000);
        public Joule ToJoule() => new(Value * 4184);
        public KiloJoule ToKiloJoule() => new(Value * 4.184);
        public KiloCalorie ToKiloCalorie() => this;

        public override string ToString() => $"{Value} {Symbol}";

        public static KiloCalorie FromCalories(double cal) => new(cal / 1000);
        public static KiloCalorie FromJoules(double joules) => new(joules / 4184);
        public static KiloCalorie FromKiloJoules(double kJ) => new(kJ / 4.184);
        public static KiloCalorie FromKiloCalories(double kiloCalories) => new(kiloCalories);
    }
}
