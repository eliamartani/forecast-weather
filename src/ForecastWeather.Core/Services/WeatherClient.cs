using ForecastWeather.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ForecastWeather.Core.Services
{
    public class WeatherClient : IWeatherClient
    {
        private readonly IConfiguration _configuration;

        public WeatherClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<WeatherResultByDay> GetData(Filter filter)
        {
            using (var client = new HttpClient())
            {
                var urlForecast = ForecastEndpoint(filter);
                var urlCurrentWeather = CurrentWeatherEndpoint(filter);
                var jsonForecast = await client.GetStringAsync(urlForecast);
                var jsonCurrent = await client.GetStringAsync(urlCurrentWeather);

                var result = ProcessForecastWeather(jsonForecast);

                // For history list
                result.Today = ProcessCurrentWeather(jsonCurrent);

                return result;
            }
        }

        private WeatherResultByDayForecastList FillListModel(WeatherResultList result) => new WeatherResultByDayForecastList
        {
            Date = DateTimeOffset.FromUnixTimeSeconds(result.Dt).DateTime,
            Icon = result.Weather.FirstOrDefault()?.Icon,
            Humidity = result.Main.Humidity,
            Temp = result.Main.Temp,
            Speed = result.Wind.Speed
        };

        private WeatherResultByDayForecastList ProcessCurrentWeather(string json)
        {
            var result = JsonConvert.DeserializeObject<WeatherResultList>(json, new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                FloatParseHandling = FloatParseHandling.Decimal
            });

            return FillListModel(result);
        }

        private WeatherResultByDay ProcessForecastWeather(string json)
        {
            var result = JsonConvert.DeserializeObject<WeatherResult>(json, new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                FloatParseHandling = FloatParseHandling.Decimal
            });

            var groupResult = result
                .List
                .GroupBy(item => DateTimeOffset.FromUnixTimeSeconds(item.Dt).Date)
                .Select(group => new WeatherResultByDayList
                {
                    Date = group.Key,
                    List = group.Select(list => FillListModel(list))
                });

            return new WeatherResultByDay
            {
                City = result.City,
                ListByDay = groupResult
            };
        }

        private string CurrentWeatherEndpoint(Filter filter)
        {
            var urlEndpoint = _configuration.GetSection("OpenWeatherMap:UrlCurrent")?.Value ?? "";
            return GetEndpointAddress(urlEndpoint, filter);
        }

        private string ForecastEndpoint(Filter filter)
        {
            var urlEndpoint = _configuration.GetSection("OpenWeatherMap:UrlForecast")?.Value ?? "";
            return GetEndpointAddress(urlEndpoint, filter);
        }
        
        private string GetEndpointAddress(string urlEndpoint, Filter filter)
        {
            var country = _configuration.GetSection("OpenWeatherMap:Country")?.Value ?? "";
            var key = _configuration.GetSection("OpenWeatherMap:AppId")?.Value ?? "";
            var units = _configuration.GetSection("OpenWeatherMap:Units")?.Value ?? "metric";
            var query = filter.GetQuery(country);

            return string.Format(urlEndpoint, query, key, units);
        }
    }
}
