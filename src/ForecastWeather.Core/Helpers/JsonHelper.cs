using System.Globalization;
using Newtonsoft.Json;

namespace ForecastWeather.Core.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                FloatParseHandling = FloatParseHandling.Decimal
            });
    }
}
