using System.Collections.Generic;
using System.Threading.Tasks;
using CoinApi.Domain.Common.Constants.ErrorCodes;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Integration.Core.Interfaces;
using CoinApi.Integration.Core.Responses;
using E.S.ApiClientHandler.Interfaces;
using E.S.ApiClientHandler.Utils;

namespace CoinApi.Integration.Services
{
    public class CoinIntegrationClientServiceBase : ICoinIntegrationClientServiceBase
    {
        private readonly IApiCachingManager _apiCachingManager;
        private readonly IApiRequestBuilder _apiRequestBuilder;

        public CoinIntegrationClientServiceBase(IApiRequestBuilder apiRequestBuilder,
            IApiCachingManager apiCachingManager)
        {
            _apiCachingManager = apiCachingManager;
            _apiRequestBuilder = apiRequestBuilder;
        }

        /// <summary>
        /// Fetch all coins from outside
        /// </summary>
        /// <param name="useCache">Indicate to use cache data</param>
        /// <returns>Integration response of coins</returns>
        public async Task<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>> GetCoinsAsync(
            bool useCache = false)
        {
            //Use request client builder to fetch results
            var response = await _apiRequestBuilder
                .New()
                .WithCacheClient(useCache ? _apiCachingManager : null, 300)
                .WithUrl("v1/cryptocurrency/map")
                .ExecuteAsync<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>>();

            //If success return results
            if (response.Success) return response.Value;

            //If failed throw custom integration exception that will handle by application layer
            if (response.Value?.Status is null)
                throw new IntegrationClientException("Failed to get coins from client", response.StatusCode,
                    IntegrationErrorCodesConstant.FailedToGetCoins);

            //If failed throw custom integration exception that will handle by application layer
            throw new IntegrationClientException("Failed to get coins from client", response.Value.Status.ErrorMessage,
                response.StatusCode, response.Value.Status.ErrorCode, IntegrationErrorCodesConstant.FailedToGetCoins);
        }

        /// <summary>
        /// Fetch all currencies from outside
        /// </summary>
        /// <param name="useCache">Indicate to use cache data</param>
        /// <returns>Integration response of currencies</returns>
        public async Task<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>> GetCurrenciesAsync(
            bool useCache = false)
        {
            //Use request client builder to fetch results
            var response = await _apiRequestBuilder
                .New()
                .WithCacheClient(useCache ? _apiCachingManager : null, 300)
                .WithUrl("v1/fiat/map")
                .ExecuteAsync<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>>();

            //If success return results
            if (response.Success) return response.Value;

            //If failed throw custom integration exception that will handle by application layer
            if (response.Value?.Status is null)
                throw new IntegrationClientException("Failed to get currencies from client", response.StatusCode,
                    IntegrationErrorCodesConstant.FailedToGetCurrencies);

            //If failed throw custom integration exception that will handle by application layer
            throw new IntegrationClientException("Failed to get currencies from client",
                response.Value.Status.ErrorMessage,
                response.StatusCode, response.Value.Status.ErrorCode,
                IntegrationErrorCodesConstant.FailedToGetCurrencies);
        }

        /// <summary>
        /// Fetch single crypto rate from outside by specifying the symbol and currencies
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="currencies">Currencies</param>
        /// <param name="useCache"></param>
        /// <returns>Integration response of crypto rates</returns>
        public async Task<IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>>
            GetCryptocurrencyAsync(
                string symbol,
                List<string> currencies,
                bool useCache = false)
        {
            //Use request client builder to fetch results
            var urlBuilder = new ApiUrlBuilder("v1/cryptocurrency/quotes/latest")
                .AddQueryValue("symbol", symbol)
                .AddQueryValue("convert", string.Join(",", currencies));

            var response = await _apiRequestBuilder
                .New()
                .WithCacheClient(useCache ? _apiCachingManager : null, 300)
                .WithUrl(urlBuilder)
                .ExecuteAsync<
                    IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>>();

            //If success return results
            if (response.Success) return response.Value;

            //If failed throw custom integration exception that will handle by application layer
            if (response.Value?.Status is null)
                throw new IntegrationClientException("Failed to get crypto currency from client", response.StatusCode,
                    IntegrationErrorCodesConstant.FailedToGetCryptoCurrency);

            //If failed throw custom integration exception that will handle by application layer
            throw new IntegrationClientException("Failed to get crypto currency from client",
                response.Value.Status.ErrorMessage,
                response.StatusCode, response.Value.Status.ErrorCode,
                IntegrationErrorCodesConstant.FailedToGetCryptoCurrency);
        }
    }
}