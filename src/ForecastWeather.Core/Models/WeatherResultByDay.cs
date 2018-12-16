using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ForecastWeather.Core.Models
{
    public class WeatherResultByDay
    {
        [JsonProperty("city")]
        public WeatherResultCity City { get; set; }

        [JsonProperty("listByDay")]
        public IEnumerable<WeatherResultByDayList> ListByDay { get; set; }

        [JsonProperty("today")]
        public WeatherResultByDayForecast Today { get; set; }
    }

    public class WeatherResultByDayList
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("forecast")]
        public WeatherResultByDayForecast Forecast { get; set; }
    }

    public class WeatherResultByDayForecast
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("humidity")]
        public float Humidity { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
