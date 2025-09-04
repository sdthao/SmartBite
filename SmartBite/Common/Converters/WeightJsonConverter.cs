using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartBite.Common.WeightMeasurement;

namespace SmartBite.Common.Converters
{
    public class WeightJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IWeightMeasurement).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var value = jsonObject["Value"]?.Value<double>();

            var symbol = jsonObject["Symbol"]?.ToString();

            if (value == null || string.IsNullOrEmpty(symbol))
            {
                throw new JsonSerializationException("Missing 'Value' or 'Symbol' property for IWeightMeasurement.");
            }

            return symbol.ToLower() switch
            {
                "g" => new Gram(value.Value),
                "kg" => new KiloGram(value.Value),
                "oz" => new Ounce(value.Value),
                "lb" => new Pound(value.Value),
                _ => throw new NotSupportedException($"Unsupported weight measurement symbol: {symbol}")
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
