using System;
using CoinApi.Hangfire.Core.Interfaces;
using CoinApi.Hangfire.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.Hangfire
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration,
            bool useInMemoryDatabase)
        {
            services.AddSingleton(new AutomaticRetryAttribute {Attempts = 0});

            if (useInMemoryDatabase)
            {
                services.AddHangfire((provider, cc) => cc
                    .UseFilter(provider.GetRequiredService<AutomaticRetryAttribute>())
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMemoryStorage());
            }
            else
            {
                services.AddHangfire((provider, cc) => cc
                    .UseFilter(provider.GetRequiredService<AutomaticRetryAttribute>())
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString("LOCAL"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                    }));
            }

            services.AddHangfireServer(options =>
            {
                options.ServerName = $"{Environment.MachineName}";
                options.Queues = new[] {"alpha", "default"};
            });

            services.AddScoped<ICoinHangfireService, CoinHangfireService>();

            return services;
        }
    }
}