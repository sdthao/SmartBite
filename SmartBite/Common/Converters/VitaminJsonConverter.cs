using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartBite.Common.NutrientMeasurement;

namespace SmartBite.Common.Converters
{
    public class VitaminJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vitamin);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var name = jsonObject["Name"]?.ToString();

            var measurementObject = jsonObject["Measurement"] as JObject;

            if (measurementObject == null || string.IsNullOrEmpty(name))
            {
                throw new JsonSerializationException("Missing 'Measurement' or 'Name' property for Vitamin.");
            }

            var value = measurementObject["Value"].Value<double>();

            var symbol = measurementObject["Symbol"]?.ToString();

            IVitaminMeasurement measurement;

            switch (symbol)
            {
                case "mg":
                    measurement = new MilliGram(value);
                    break;
                case "mcg":
                    measurement = new MicroGram(value);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported vitamin measurement symbol: {symbol}");
            }

            return name switch
            {
                "Vitamin A" => new VitaminA(measurement),
                //"Vitamin B1" => new VitaminB1(measurement),
                //"Vitamin B2" => new VitaminB2(measurement),
                //"Vitamin B3" => new VitaminB3(measurement),
                //"Vitamin B5" => new VitaminB5(measurement),
                //"Vitamin B6" => new VitaminB6(measurement),
                //"Vitamin B7" => new VitaminB7(measurement),
                //"Vitamin B9" => new VitaminB9(measurement),
                //"Vitamin B12" => new VitaminB12(measurement),
                "Vitamin C" => new VitaminC(measurement),
                "Vitamin D" => new VitaminD(measurement),
                //"Vitamin E" => new VitaminE(measurement),
                //"Vitamin K" => new VitaminK(measurement),
                _ => throw new NotSupportedException($"Unsupported vitamin name: {name}")
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
