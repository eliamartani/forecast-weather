using ForecastWeather.Core.Models;
using ForecastWeather.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;

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
        /// <returns>WeatherResultByDay representation</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<WeatherResultByDay> Forecast(
            string city = null,
            string countryCode = null,
            string zipCode = null)
        {
            var filter = new Filter(city, countryCode, zipCode);
            var response = await _weatherClient.GetData(filter);

            return response;
        }
    }
}
