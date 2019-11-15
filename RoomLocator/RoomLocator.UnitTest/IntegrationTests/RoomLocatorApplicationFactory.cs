using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoomLocator.Api.Helpers;
using RoomLocator.Data.Config;

namespace RoomLocator.UnitTest.IntegrationTests
{
    public class RoomLocatorApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(RoomLocatorContext));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<RoomLocatorContext>((options, context) =>
                {
                    context.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<RoomLocatorContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<RoomLocatorApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        DatabaseSeedHelper.SeedRoles(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"As error occurred seeding the database with test data. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
