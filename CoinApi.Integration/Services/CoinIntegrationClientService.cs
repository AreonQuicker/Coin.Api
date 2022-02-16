using CoinApi.Integration.Core.Interfaces;
using E.S.ApiClientHandler.Config;
using E.S.ApiClientHandler.Core;
using E.S.ApiClientHandler.Interfaces;

namespace CoinApi.Integration.Services
{
    public class CoinIntegrationClientService : CoinIntegrationClientServiceBase, ICoinIntegrationClientService
    {
        public CoinIntegrationClientService(ICoinClientService coinClientService, IApiCachingManager apiCachingManager)
            : base(ApiRequestBuilder.Make(coinClientService.HttpClient,
                coinClientService.HttpClient.BaseAddress.ToString(),
                ApiRequestBuilderConfig.Create(false, null)), apiCachingManager)
        {
        }
    }
}