using ForecastWeather.Core.Models;
using ForecastWeather.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForecastWeather.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ResponseCache(Duration = 3600, VaryByQueryKeys = new [] { "city", "zipCode" })]
    [Route("api/[controller]/[action]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherClient _weatherClient;

        public WeatherController(IWeatherClient weatherClient) => _weatherClient = weatherClient;

        /// <summary>
        /// Provides weather forecast for 5 days.
        /// User must provide city name or zip code in order to the request works well.
        /// </summary>
        /// <example>
        /// GET: api/Weather/Forecast?city=Leipzig
        /// GET: api/Weather/Forecast?city=Leipzig&countryCode=de
        /// GET: api/Weather/Forecast?zipcode=04109
        /// GET: api/Weather/Forecast?zipcode=04109&countryCode=de
        /// </example>
        /// <param name="city">City name</param>
        /// <param name="countryCode">(Optional) Country code composed by 2 letters</param>
        /// <param name="zipCode">Postal code</param>
        /// <returns>WeatherResult representation</returns>
        [HttpGet]
        public async Task<ObjectResult> Forecast(
            string city = null,
            string countryCode = null,
            string zipCode = null) => Ok(new DataResult
            {
                Message = "Data retrieved with success",
                Data = await _weatherClient.GetData(new Filter
                {
                    City = city,
                    CountryCode = countryCode,
                    ZipCode = zipCode
                })
            });
    }
}
