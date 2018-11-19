using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ForecastWeather.WebApi.Config
{
    public static class SwaggerHelper
    {
        public static void ConfigureSwagger(this IServiceCollection services) => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Info
            {
                Title = "ForecastWeather.WebApi",
                Version = "1.0.0",
                Contact = new Contact
                {
                    Email = "eliamartani@gmail.com",
                    Name = "Eliamar Tani",
                    Url = "https://github.com/eliamartani/"
                }
            });
        });

        public static void Swagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI");
            });
        }
    }
}
