using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.Background.Queue.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.Background.Queue
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBackgroundQueue(this IServiceCollection services)
        {
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundQueue>(_ => new BackgroundQueue(5000));

            return services;
        }
    }
}