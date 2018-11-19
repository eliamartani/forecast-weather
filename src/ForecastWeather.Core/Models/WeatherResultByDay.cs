using System;
using System.Collections.Generic;

namespace ForecastWeather.Core.Models
{
    public class WeatherResultByDay
    {
        public WeatherResultCity City { get; set; }
        public IEnumerable<WeatherResultByDayList> ListByDay { get; set; }
        public WeatherResultByDayForecastList Today { get; set; }
    }

    public class WeatherResultByDayList
    {
        public DateTime Date { get; set; }
        public IEnumerable<WeatherResultByDayForecastList> List { get; set; }
    }

    public class WeatherResultByDayForecastList
    {
        public DateTime Date { get; set; }
        public string Icon { get; set; }
        public float Humidity { get; set; }
        public double Temp { get; set; }
        public double Speed { get; set; }
    }
}
