using ForecastWeather.Core.Exceptions;
using System.Web;

namespace ForecastWeather.Core.Models
{
    public class Filter
    {
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string ZipCode { get; set; }

        public Filter() { }

        public Filter(string city, string countryCode, string zipCode)
        {
            City = city;
            CountryCode = countryCode;
            ZipCode = zipCode;

            if (string.IsNullOrWhiteSpace(City) && string.IsNullOrWhiteSpace(ZipCode))
            {
                throw new NoEntryProvidedException();
            }
        }

        public string GetQuery(string countryCode = null)
        {
            bool hasZipCode = !string.IsNullOrWhiteSpace(ZipCode);
            string country = string.Concat(",", CountryCode ?? countryCode); // Priority order
            string queryBy = hasZipCode ? "zip" : "q";
            string value = hasZipCode ? ZipCode : City;
            string queryCountry = value.EndsWith(country) ? string.Empty : country;
            string queryValue = $"{ value }{ queryCountry }";
            string query = $"{ queryBy }={ HttpUtility.UrlEncode(queryValue) }";

            return query;
        }
    }
}
