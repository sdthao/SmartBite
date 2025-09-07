using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartBite.Common.Converters;

namespace SmartBite.Extensions
{
    public static class SmartBiteServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<CalorieJsonConverter>();
            services.AddSingleton<WeightJsonConverter>();
            services.AddSingleton<VitaminJsonConverter>();

            services.AddSingleton(x =>
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>
                    {
                        x.GetRequiredService<CalorieJsonConverter>(),
                        x.GetRequiredService<WeightJsonConverter>(),
                        x.GetRequiredService<VitaminJsonConverter>()
                    },
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                return settings;
            });

            return services;
        }
    }
}
