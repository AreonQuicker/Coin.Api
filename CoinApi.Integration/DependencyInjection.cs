using System.Net;
using System.Net.Http;
using CoinApi.Integration.Core.Interfaces;
using CoinApi.Integration.Services;
using E.S.ApiClientHandler.Interfaces;
using E.S.ApiClientHandler.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICoinClientService, CoinClientService>().ConfigurePrimaryHttpMessageHandler(() =>
                new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                });
            services.AddSingleton<IApiCachingManager, ApiMemoryCachingManager>();
            services.AddSingleton<ICoinIntegrationClientService, CoinIntegrationClientService>();
            services.Decorate<ICoinIntegrationClientService, CoinIntegrationClientPollyService>();

            return services;
        }
    }
}