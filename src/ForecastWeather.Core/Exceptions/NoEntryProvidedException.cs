using System;

namespace ForecastWeather.Core.Exceptions
{
    public class NoEntryProvidedException : Exception
    {
        public override string Message => "No data entry was provided.";
    }
}
