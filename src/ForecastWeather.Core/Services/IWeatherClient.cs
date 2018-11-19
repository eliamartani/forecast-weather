using ForecastWeather.Core.Models;
using System.Threading.Tasks;

namespace ForecastWeather.Core.Services
{
    public interface IWeatherClient
    {
        Task<WeatherResultByDay> GetData(Filter filter);
    }
}
