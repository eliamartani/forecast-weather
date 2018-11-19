using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ForecastWeather.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ForecastWeather.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (HttpRequestException)
            {
                // When user provides a city which doesn't exist, the API throws an exception.
                // Treat it so the user return will be an empty array.
                await HandleExceptionAsync(context, "No city found", (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, int? errorCode = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCode.GetValueOrDefault((int)HttpStatusCode.InternalServerError);

            await context.Response.WriteAsync(ToJson(message));
        }

        private string ToJson(string message) => JsonConvert.SerializeObject(new DataResult(message, null));
    }
}
