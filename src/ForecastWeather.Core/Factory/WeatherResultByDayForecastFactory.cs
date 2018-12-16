using System;
using System.Linq;
using ForecastWeather.Core.Models;

namespace ForecastWeather.Core.Factory
{
    public class WeatherResultByDayForecastFactory
    {
        public static WeatherResultByDayForecast Create(WeatherResultList result) => new WeatherResultByDayForecast
        {
            Date = DateTimeOffset.FromUnixTimeSeconds(result.Dt).DateTime,
            Icon = result.Weather.FirstOrDefault()?.Icon,
            Humidity = result.Main.Humidity,
            Temp = result.Main.Temp,
            Speed = result.Wind.Speed
        };

        public static WeatherResultByDayForecast Create(IGrouping<DateTime, WeatherResultList> group) => new WeatherResultByDayForecast
        {
            Date = group.Key,
            Humidity = group.Average(a => a.Main.Humidity),
            Icon = group.FirstOrDefault()?.Weather.FirstOrDefault()?.Icon,
            Speed = group.Average(a => a.Wind.Speed),
            Temp = group.Average(a => a.Main.Temp)
        };
    }
}
