using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// This file contains generated structure from openweathermap
/// </summary>
namespace ForecastWeather.Core.Models
{
    public class WeatherResultMain
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("humidity")]
        public float Humidity { get; set; }
    }

    public class WeatherResultWeather
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class WeatherResultWind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }

    public class WeatherResultList
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("main")]
        public WeatherResultMain Main { get; set; }

        [JsonProperty("wind")]
        public WeatherResultWind Wind { get; set; }

        [JsonProperty("weather")]
        public List<WeatherResultWeather> Weather { get; set; }
    }

    public class WeatherResultCity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class WeatherResult
    {
        [JsonProperty("list")]
        public List<WeatherResultList> List { get; set; }

        [JsonProperty("city")]
        public WeatherResultCity City { get; set; }
    }
}
