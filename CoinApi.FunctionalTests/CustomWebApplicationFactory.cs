using System;
using System.Linq;
using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.DataAccess;
using CoinApi.Hangfire.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoinApi.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        /// <summary>
        ///     Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = builder.Build();

            // Get service provider.
            var serviceProvider = host.Services;

            // Create a scope to obtain a reference to the database
            // context (AppDbContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DataContext>();

                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.        
                    TestHelpers.SeedDataAsync(db).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                                        $"database with test messages. Error: {ex.Message}");
                }
            }

            host.Start();
            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseSolutionRelativeContentRoot("CoinApi")
                .ConfigureServices(services =>
                {
                    // Remove the app's ApplicationDbContext registration.
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<DataContext>));

                    if (descriptor != null) services.Remove(descriptor);

                    var backgroundQueue = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(IBackgroundQueue));

                    // if (backgroundQueue != null)
                    // {
                    //     services.Remove(backgroundQueue);
                    // }
                    //
                    var coinHangfireService = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(ICoinHangfireService));

                    if (coinHangfireService != null) services.Remove(coinHangfireService);
                    //
                    // services.AddSingleton(provider =>
                    //     Mock.Of<IBackgroundQueue>()); 
                    //
                    services.AddSingleton(provider =>
                        Mock.Of<ICoinHangfireService>());

                    // This should be set for each individual test run
                    var inMemoryCollectionName = Guid.NewGuid().ToString();

                    // Add ApplicationDbContext using an in-memory database for testing.
                    services.AddDbContext<DataContext>(options =>
                    {
                        options.UseInMemoryDatabase(inMemoryCollectionName);
                    });
                });
        }
    }
}