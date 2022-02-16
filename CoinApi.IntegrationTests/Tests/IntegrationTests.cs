using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CoinApi.Domain.Common.Configurations;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Integration.Core.Interfaces;
using CoinApi.Integration.Services;
using E.S.ApiClientHandler.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CoinApi.IntegrationTests.Tests
{
    [Collection("Integration")]
    public class IntegrationTests : TestBase, IClassFixture<TestFactory>
    {
        private readonly ICoinClientService _coinClientService;
        private readonly HttpClient _testClient;

        public IntegrationTests(TestFactory testFactory) : base(testFactory)
        {
            _testClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            var apiConfig = Configuration.GetSection("CoinApiConfig").Get<CoinApiConfiguration>();

            IOptions<CoinApiConfiguration> options =
                new OptionsWrapper<CoinApiConfiguration>(apiConfig);

            _coinClientService = new CoinClientService(_testClient, options);
        }

        [Theory]
        [InlineAutoData("BTC")]
        public async Task GetCoins_ShouldReturnResult_WithValidQueury(string symbolToBeExpect, IFixture fixture,
            [Frozen] Mock<IApiCachingManager> apiCachingManagerMock)
        {
            fixture.Customize(new AutoMoqCustomization());
            fixture.Inject(_coinClientService);

            var coinIntegrationClientService = fixture.Create<CoinIntegrationClientService>();

            var results = await coinIntegrationClientService.GetCoinsAsync();

            results.Should().NotBeNull();
            results.Data.Should().NotBeNull();
            results.Status.Should().NotBeNull();
            results.Data.Should().NotBeEmpty();
            results.Data.Should().Contain(t =>
                t.Symbol.Equals(symbolToBeExpect, StringComparison.InvariantCultureIgnoreCase));
        }

        [Theory]
        [InlineAutoData("USD")]
        public async Task GetCurrencies_ShouldReturnResult_WithValidQueury(string symbolToBeExpect, IFixture fixture,
            [Frozen] Mock<IApiCachingManager> apiCachingManagerMock)
        {
            fixture.Customize(new AutoMoqCustomization());
            fixture.Inject(_coinClientService);

            var coinIntegrationClientService = fixture.Create<CoinIntegrationClientService>();

            var results = await coinIntegrationClientService.GetCurrenciesAsync();

            results.Should().NotBeNull();
            results.Data.Should().NotBeNull();
            results.Status.Should().NotBeNull();
            results.Data.Should().NotBeEmpty();
            results.Data.Should().Contain(t =>
                t.Symbol.Equals(symbolToBeExpect, StringComparison.InvariantCultureIgnoreCase));
        }

        [Theory]
        [InlineAutoData("BTC", "USD")]
        public async Task GetCryptoCurrency_ShouldReturnResult_WithValidQueury(string symbolToBeExpect,
            string currencyToBeExpect, IFixture fixture, [Frozen] Mock<IApiCachingManager> apiCachingManagerMock)
        {
            fixture.Customize(new AutoMoqCustomization());
            fixture.Inject(_coinClientService);

            var coinIntegrationClientService = fixture.Create<CoinIntegrationClientService>();

            var results = await coinIntegrationClientService.GetCryptocurrencyAsync("BTC", new List<string> {"USD"});

            results.Should().NotBeNull();
            results.Data.Should().NotBeNull();
            results.Status.Should().NotBeNull();
            results.Data.Should().NotBeEmpty();

            results.Data.Values.Should().NotBeNull();
            results.Data.Values.Should().NotBeEmpty();
            results.Data.Values.Should().Contain(t => t.Symbol == symbolToBeExpect);

            results.Data.Values.FirstOrDefault()!.Quotes.Should().NotBeNull();
            results.Data.Values.FirstOrDefault()!.Quotes.Should().NotBeEmpty();
            results.Data.Values.FirstOrDefault()!.Quotes.Should().Contain(c => c.Key == currencyToBeExpect);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetCryptoCurrency_ShouldThrowException_WithInValidQueury(IFixture fixture,
            [Frozen] Mock<IApiCachingManager> apiCachingManagerMock)
        {
            fixture.Customize(new AutoMoqCustomization());
            fixture.Inject(_coinClientService);

            var coinIntegrationClientService = fixture.Create<CoinIntegrationClientService>();

            Func<Task> f = async () =>
            {
                await coinIntegrationClientService.GetCryptocurrencyAsync("OOO", new List<string> {"OOO"});
            };

            await f.Should().ThrowAsync<IntegrationClientException>()
                .Where(w => w.StatusCode == HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetCryptoCurrency_ShouldThrowException_WithNoKey(IFixture fixture,
            [Frozen] Mock<IApiCachingManager> apiCachingManagerMock)
        {
            _coinClientService.HttpClient.DefaultRequestHeaders.Remove("X-CMC_PRO_API_KEY");

            fixture.Customize(new AutoMoqCustomization());
            fixture.Inject(_coinClientService);

            var coinIntegrationClientService = fixture.Create<CoinIntegrationClientService>();

            Func<Task> f = async () =>
            {
                await coinIntegrationClientService.GetCryptocurrencyAsync("OOO", new List<string> {"OOO"});
            };


            await f.Should().ThrowAsync<IntegrationClientException>()
                .Where(w => w.StatusCode == HttpStatusCode.Unauthorized);
        }
    }
}