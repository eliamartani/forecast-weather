using System.Globalization;
using Newtonsoft.Json;

namespace ForecastWeather.Core.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            var settings = new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                FloatParseHandling = FloatParseHandling.Decimal
            };
            var parsedObject = JsonConvert.DeserializeObject<T>(json, settings);

            return parsedObject;
        }
    }
}
