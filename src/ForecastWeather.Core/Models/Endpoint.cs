namespace ForecastWeather.Core.Models
{
    public class Endpoint
    {
        public string Forecast { get; }
        public string Weather { get; }

        public Endpoint(string forecast, string weather)
        {
            Forecast = forecast;
            Weather = weather;
        }
    }
}
