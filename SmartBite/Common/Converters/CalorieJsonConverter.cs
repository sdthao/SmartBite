using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartBite.Common.CalorieMeasurement;

namespace SmartBite.Common.Converters
{
    public class CalorieJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(ICalorieMeasurement).IsAssignableFrom(objectType);
        }
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var value = jsonObject["Value"]?.Value<double>();

            var symbol = jsonObject["Symbol"]?.ToString();

            if (value == null || string.IsNullOrEmpty(symbol))
            {
                throw new JsonSerializationException("Missing 'Value' or 'Symbol' property for ICalorieMeasurement.");
            }

            return symbol.ToLower() switch
            {
                "cal" => new Calorie(value.Value),
                "kcal" => new KiloCalorie(value.Value),
                "kj" => new KiloJoule(value.Value),
                "j" => new Joule(value.Value),
                _ => throw new NotSupportedException($"Unsupported calorie measurement symbol: {symbol}")
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
