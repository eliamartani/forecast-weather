namespace ForecastWeather.Core.Models
{
    public class DataResult
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public DataResult()
        {
        }

        public DataResult(string message, object data)
        {
            Message = message;
            Data = data;
        }
    }
}
