using System.Collections.Generic;

/// <summary>
/// This file contains generated structure from openweathermap
/// </summary>
namespace ForecastWeather.Core.Models
{
    public class WeatherResultMain
    {
        public double Temp { get; set; }
        public float Humidity { get; set; }
    }

    public class WeatherResultWeather
    {
        public string Icon { get; set; }
    }

    public class WeatherResultWind
    {
        public double Speed { get; set; }
    }

    public class WeatherResultList
    {
        public long Dt { get; set; }
        public WeatherResultMain Main { get; set; }
        public WeatherResultWind Wind { get; set; }
        public List<WeatherResultWeather> Weather { get; set; }
    }

    public class WeatherResultCoord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class WeatherResultCity
    {
        public string Name { get; set; }
        public WeatherResultCoord Coord { get; set; }
        public string Country { get; set; }
    }

    public class WeatherResult
    {
        public List<WeatherResultList> List { get; set; }
        public WeatherResultCity City { get; set; }
    }
}
