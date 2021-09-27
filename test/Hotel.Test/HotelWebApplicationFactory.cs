using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

using Hotel.Data.Contexts;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Test
{
    public class HotelWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => 
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HotelContext>));

                services.Remove(descriptor);

                services.AddDbContext<HotelContext>(options =>
                {
                    options.UseSqlite("Filename=hotel.db", options =>
                     {
                         options.MigrationsAssembly(typeof(HotelContext).Assembly.FullName);
                     });
                });

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<HotelContext>();

                    var state = context.Database.GetDbConnection().State;

                    if (state == System.Data.ConnectionState.Closed)
                    {
                        context.Database.OpenConnection();
                    }

                    context.Database.EnsureCreated();
                    context.Database.ExecuteSqlRaw("DELETE FROM Reservations");
                }
            });

            base.ConfigureWebHost(builder);
        }
    }
}
