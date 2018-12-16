using ForecastWeather.Core.Services;
using ForecastWeather.WebApi.Config;
using ForecastWeather.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Cache
            services.AddResponseCaching();

            // CORS
            services.AddCors(opt => opt.AddPolicy("ApiPolicyCors", builder => builder.AllowAnyOrigin()));

            // HTTP Client exclusively for "ForecastWebApi"
            services.AddHttpClient("ForecastWebApi");

            // Dependency Injection
            services.AddScoped(typeof(IWeatherClient), typeof(WeatherClient));

            // Swagger
            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                services.ConfigureSwagger();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Swagger
            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                app.Swagger();
            }

            // Cache middleware
            app.UseResponseCaching();

            // CORS
            app.UseCors("ApiPolicyCors");

            // HTTPS
            app.UseHttpsRedirection();

            // Exception Handler
            app.UseMiddleware<ExceptionMiddleware>();


            app.UseMvc();
        }
    }
}
