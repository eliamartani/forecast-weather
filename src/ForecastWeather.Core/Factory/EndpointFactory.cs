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
            var urlCurrent = configuration.GetSection("OpenWeatherMap:UrlCurrent")?.Value ?? "";
            var urlForecast = configuration.GetSection("OpenWeatherMap:UrlForecast")?.Value ?? "";

            var query = filter.GetQuery(country);
            var forecast = string.Format(urlForecast, query, key, units);
            var weather = string.Format(urlCurrent, query, key, units);

            return new Endpoint(forecast, weather);
        }
    }
}
