using System;
using System.Net.Http;
using CoinApi.Domain.Common.Configurations;
using CoinApi.Integration.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CoinApi.Integration.Services
{
    /// <summary>
    /// Build up a Http Client Service
    /// </summary>
    public class CoinClientService : ICoinClientService
    {
        public CoinClientService(HttpClient httpClient, IOptions<CoinApiConfiguration> options)
        {
            HttpClient = httpClient;

            HttpClient.BaseAddress = new Uri(options.Value.BaseUrl);

            HttpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/json");
            HttpClient.DefaultRequestHeaders.Add(
                "Accept-Encoding", "deflate, gzip");
            HttpClient.DefaultRequestHeaders.Add(
                "X-CMC_PRO_API_KEY", options.Value.Key);
        }

        public HttpClient HttpClient { get; }
    }
}