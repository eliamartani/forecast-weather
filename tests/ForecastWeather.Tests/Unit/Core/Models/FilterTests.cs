using ForecastWeather.Core.Exceptions;
using ForecastWeather.Core.Models;
using Xunit;

namespace ForecastWeather.Tests.Unit.Core.Models
{
    public class FilterTests
    {
        [Fact]
        public void ValidatesCity()
        {
            var city = new Filter
            {
                City = "Leipzig"
            };

            Assert.Equal($"q=Leipzig%2c", city.GetQuery());
            Assert.Equal($"q=Leipzig%2cde", city.GetQuery("de"));
            Assert.Equal($"q=Leipzig%2cus", city.GetQuery("us"));
        }

        [Fact]
        public void ValidatesCityWithCountry()
        {
            var city = new Filter
            {
                City = "Leipzig",
                CountryCode = "de"
            };

            Assert.Equal($"q=Leipzig%2cde", city.GetQuery());
            Assert.Equal($"q=Leipzig%2cde", city.GetQuery("us"));
        }

        [Fact]
        public void ValidatesZipCode()
        {
            var city = new Filter
            {
                ZipCode = "04109"
            };

            Assert.Equal($"zip=04109%2c", city.GetQuery());
            Assert.Equal($"zip=04109%2cde", city.GetQuery("de"));
            Assert.Equal($"zip=04109%2cus", city.GetQuery("us"));
        }

        [Fact]
        public void ValidatesZipCodeWithCountry()
        {
            var city = new Filter
            {
                CountryCode = "de",
                ZipCode = "04109"
            };

            Assert.Equal($"zip=04109%2cde", city.GetQuery());
            Assert.Equal($"zip=04109%2cde", city.GetQuery("us"));
        }

        [Fact]
        public void ValidatesException()
        {
            // Occurrs when city neither zipcode is provided
            Assert.Throws<NoEntryProvidedException>(() =>
            {
                var filter = new Filter();

                filter.GetQuery();
            });
        }
    }
}
