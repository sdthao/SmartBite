using SmartBite.Common.Measurement;

namespace SmartBite.Common.CalorieMeasurement
{
    public record KiloJoule(double Value) : ICalorieMeasurement
    {
        public string Name => "KiloJoule";
        public string Symbol => "kJ";

        public Joule ToJoule() => new(Value * 1000);
        public Calorie ToCalorie() => new((Value * 1000) / 4.184);
        public KiloCalorie ToKiloCalorie() => new(Value / 4.184);
        public KiloJoule ToKiloJoule() => this;

        public static KiloJoule FromKiloJoules(double value) => new(value);
        public static KiloJoule FromCalories(double value) => new((value * 4.184) / 1000);
        public static KiloJoule FromKiloCalories(double value) => new(value * 4.184);
        public static KiloJoule FromJoules(double value) => new(value / 1000);

        public override string ToString() => $"{Value} {Symbol}";
    }
}
