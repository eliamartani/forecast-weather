using ForecastWeather.Core.Services;
using ForecastWeather.WebApi.Config;
using ForecastWeather.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForecastWeather.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddResponseCaching();

            services.AddCors(opt => opt.AddPolicy("ApiPolicyCors", builder => builder.AllowAnyOrigin()));

            // HTTP Client exclusively for "ForecastWebApi"
            services.AddHttpClient("ForecastWebApi");

            // Dependency Injection
            services.AddScoped(typeof(IWeatherClient), typeof(WeatherClient));

            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                services.ConfigureSwagger();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                app.Swagger();
            }

            app.UseResponseCaching();

            app.UseCors("ApiPolicyCors");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
