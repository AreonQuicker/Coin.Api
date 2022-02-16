using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using CoinApi.Domain.Currency.Models;
using CoinApi.FunctionalTests.Models;
using FluentAssertions;
using Xunit;

namespace CoinApi.FunctionalTests.Tests
{
    [Collection("CurrencyControllerTests")]
    public class CurrencyControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CurrencyControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCurrencies_ShouldReturnResult()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<ClientPaginatedList<CurrencyResult>>>(
                    "/api/Currency");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Items.Should().NotBeNull();
            result.Data.Items.Should().NotBeEmpty();
            result.Data.Items.Should().HaveCount(2);
        }

        [Theory]
        [InlineData("Symbol")]
        [InlineData("Symbol2")]
        public async Task GetCurrencyBySymbol_ShouldReturnResult(string symbol)
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<CurrencyResult>>("/api/Currency/Symbol/" + symbol);

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Symbol.Should().Be(symbol);
        }

        [Theory]
        [InlineData("OOO")]
        public async Task GetCurrencyBySymbol_ShouldReturnNoResult(string symbol)
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<CurrencyResult>>("/api/Currency/Symbol/" + symbol);

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().BeNull();
        }
    }
}