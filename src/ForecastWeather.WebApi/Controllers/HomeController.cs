using Microsoft.AspNetCore.Mvc;

namespace ForecastWeather.WebApi.Controllers
{
    [ApiController]
    [ResponseCache(Duration = 3600)]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        // GET: api/Home
        [HttpGet]
        public string Get() => "There is no place like home...";
    }
}
