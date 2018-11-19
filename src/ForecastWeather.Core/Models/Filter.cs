using ForecastWeather.Core.Exceptions;
using System.Web;

namespace ForecastWeather.Core.Models
{
    public class Filter
    {
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string ZipCode { get; set; }

        public string GetQuery(string countryCode = null)
        {
            if (string.IsNullOrWhiteSpace(City) && string.IsNullOrWhiteSpace(ZipCode))
            {
                throw new NoEntryProvidedException();
            }

            string country = string.Concat(",", CountryCode ?? countryCode); // Priority order
            string queryBy = "q";
            string value = City;

            if (!string.IsNullOrWhiteSpace(ZipCode))
            {
                queryBy = "zip";
                value = ZipCode;
            }

            if (value.EndsWith(country))
            {
                country = string.Empty;
            }
            
            return $"{ queryBy }={ HttpUtility.UrlEncode($"{ value }{ country }") }";
        }
    }
}
