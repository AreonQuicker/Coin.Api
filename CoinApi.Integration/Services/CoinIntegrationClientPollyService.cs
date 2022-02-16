using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CoinApi.Domain.Common.Constants.ErrorCodes;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Integration.Core.Interfaces;
using CoinApi.Integration.Core.Responses;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;

namespace CoinApi.Integration.Services
{
    /// <summary>
    /// Client integration polly Service
    /// Retry policy will be used for all bad requests. Max limit of 3 retries
    /// Circuit Breaker policy will be used for forbidden, payment required requests
    /// </summary>
    public class CoinIntegrationClientPollyService : ICoinIntegrationClientService
    {
        private const int MaxRetries = 3;
        private readonly ICoinIntegrationClientService _coinIntegrationClientService;

        public CoinIntegrationClientPollyService(ICoinIntegrationClientService coinIntegrationClientService)
        {
            _coinIntegrationClientService = coinIntegrationClientService;
        }

        public async Task<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>> GetCoinsAsync(
            bool useCache = false)
        {
            //If circuit breaker is still open, throw Integration exception for service unavailable
            if (CircuitBreakerPolicy.CircuitState == CircuitState.Open)
                throw new IntegrationClientException("Service is currently unavailable", null,
                    IntegrationErrorCodesConstant.FailedToGetCoins);

            var data = await MainPolicy.ExecuteAsync(async () =>
            {
                return await _coinIntegrationClientService.GetCoinsAsync(useCache);
            });

            return data;
        }

        public async Task<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>> GetCurrenciesAsync(
            bool useCache = false)
        {
            //If circuit breaker is still open, throw Integration exception for service unavailable
            if (CircuitBreakerPolicy.CircuitState == CircuitState.Open)
                throw new IntegrationClientException("Service is currently unavailable", null,
                    IntegrationErrorCodesConstant.FailedToGetCoins);

            var data = await MainPolicy.ExecuteAsync(async () =>
            {
                return await _coinIntegrationClientService.GetCurrenciesAsync(useCache);
            });

            return data;
        }

        public async Task<IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>>
            GetCryptocurrencyAsync(
                string symbol, List<string> currencies, bool useCache = false)
        {
            //If circuit breaker is still open, throw Integration exception for service unavailable
            if (CircuitBreakerPolicy.CircuitState == CircuitState.Open)
                throw new IntegrationClientException("Service is currently unavailable", null,
                    IntegrationErrorCodesConstant.FailedToGetCoins);

            var data = await MainPolicy.ExecuteAsync(async () =>
            {
                return await _coinIntegrationClientService.GetCryptocurrencyAsync(symbol, currencies, useCache);
            });

            return data;
        }

        #region Private Static

        /// <summary>
        /// Retry policy
        /// </summary>
        private static readonly AsyncRetryPolicy
            RetryPolicy = Policy
                .Handle<IntegrationClientException>(exception =>
                {
                    return exception.StatusCode != null && (exception.StatusCode == HttpStatusCode.BadRequest ||
                                                            (int) exception.StatusCode >= 500);
                }).WaitAndRetryAsync(MaxRetries, time => TimeSpan.FromSeconds(time * 1));

        /// <summary>
        /// Circuit breaker policy
        /// </summary>
        private static readonly AsyncCircuitBreakerPolicy
            CircuitBreakerPolicy = Policy
                .Handle<IntegrationClientException>(exception =>
                {
                    return exception.StatusCode is HttpStatusCode.TooManyRequests or HttpStatusCode.Forbidden or
                        HttpStatusCode.PaymentRequired;
                })
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(10));

        private static readonly AsyncPolicyWrap
            MainPolicy = CircuitBreakerPolicy.WrapAsync(RetryPolicy);

        #endregion
    }
}