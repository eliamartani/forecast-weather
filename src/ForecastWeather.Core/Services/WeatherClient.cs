using ForecastWeather.Core.Factory;
using ForecastWeather.Core.Helpers;
using ForecastWeather.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ForecastWeather.Core.Services
{
  public class WeatherClient : IWeatherClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public WeatherClient(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ForecastWebApi");
            _configuration = configuration;
        }

        public async Task<WeatherResultByDay> GetData(Filter filter)
        {
            // Creates the endpoint
            var endpoint = EndpointFactory.Create(filter, _configuration);

            // Get json object concerning 5 days forecast from API
            var jsonForecast = await _client.GetStringAsync(endpoint.Forecast);

            // Get json object concerning today's weather from API
            var jsonCurrent = await _client.GetStringAsync(endpoint.Weather);

            // Parse data from forecat
            var forecastResult = JsonHelper.Deserialize<WeatherResult>(jsonForecast);

            // Average parse from forecast list
            var groupResult = forecastResult
                .List
                .GroupBy(item => DateTimeOffset.FromUnixTimeSeconds(item.Dt).Date)
                .Select(group => new WeatherResultByDayList
                {
                    Date = group.Key,
                    Forecast = WeatherResultByDayForecastFactory.Create(group)
                });

            var result = new WeatherResultByDay
            {
                City = forecastResult.City,
                ListByDay = groupResult
            };

            // Parse data from today's weather
            result.Today = WeatherResultByDayForecastFactory.Create(
                JsonHelper.Deserialize<WeatherResultList>(jsonCurrent)
            );

            return result;
        }
    }
}
