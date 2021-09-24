using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

using Hotel.Api.Middlewares;
using Hotel.CrossCutting.IoC;
using Hotel.Data.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Hotel.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        #region Public Methods
        public static IServiceCollection AddAndConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<HotelContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<HotelContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Hotel.Data"))
            );

            var dependencyManager = new DependencyManager(new DependencyManagerContext(services, configuration));
            dependencyManager.BuildContainer();

            return services;
        }

        public static IServiceCollection AddAndConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers()
            .AddDataAnnotationsLocalization()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return services;
        }

        public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
        {
            var version = "v1";

            services.AddSwaggerGen(options =>
            {
                var info = new OpenApiInfo()
                {
                    Title = "Hotel Booking Web API",
                    Version = version,
                    Description = "A Web API for Booking in a Cancun Hotel."
                };

                options.SwaggerDoc(version, info);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            return services;
        } 
        #endregion
    }
}
