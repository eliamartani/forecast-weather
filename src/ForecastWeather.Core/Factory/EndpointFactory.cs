using ForecastWeather.Core.Models;
using Microsoft.Extensions.Configuration;

namespace ForecastWeather.Core.Factory
{
  public class EndpointFactory
    {
        public static Endpoint Create(Filter filter, IConfiguration configuration)
        {
            var country = configuration.GetSection("OpenWeatherMap:Country")?.Value ?? "";
            var key = configuration.GetSection("OpenWeatherMap:AppId")?.Value ?? "";
            var units = configuration.GetSection("OpenWeatherMap:Units")?.Value ?? "metric";
            var query = filter.GetQuery(country);

            var weather = string.Format(configuration.GetSection("OpenWeatherMap:UrlCurrent")?.Value ?? "", query, key, units);
            var forecast = string.Format(configuration.GetSection("OpenWeatherMap:UrlForecast")?.Value ?? "", query, key, units);

            return new Endpoint(forecast, weather);
        }
    }
}
