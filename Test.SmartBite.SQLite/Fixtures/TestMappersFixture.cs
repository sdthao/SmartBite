using Newtonsoft.Json;
using SmartBite.Common;
using SmartBite.Models.Food;
using SmartBite.Models.User;
using SmartBite.SQLite.Models;
using SmartBite.SQLite.Mappers;
using SmartBite.Common.Converters;

namespace Test.SmartBit.SQLite.Fixtures
{
    public class TestMappersFixture
    {
        public IMapper<FoodItem, FoodItemEntity> FoodItemMapper { get; private set; }

        public IMapper<FoodItemEntity, FoodItem> FoodItemEntityMapper { get; private set; }

        public IMapper<UserProfile, UserProfileEntity> UserProfileMapper { get; private set; }

        public IMapper<UserProfileEntity, UserProfile> UserProfileEntityMapper { get; private set; }

        public JsonSerializerSettings JsonSettings { get; private set; }

        public TestMappersFixture()
        {
            JsonSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new CalorieJsonConverter(),
                    new WeightJsonConverter(),
                    new VitaminJsonConverter()
                },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            FoodItemMapper = new FoodItemMapper(JsonSettings);
            FoodItemEntityMapper = new FoodItemEntityMapper(JsonSettings);
            UserProfileMapper = new UserProfileMapper();
            UserProfileEntityMapper = new UserProfileEntityMapper();
        }
    }
}
