using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using CoinApi.Domain.Coin.Models;
using CoinApi.FunctionalTests.Models;
using FluentAssertions;
using Xunit;

namespace CoinApi.FunctionalTests.Tests
{
    [Collection("CoinControllerTests")]
    public class CoinControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CoinControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCoins_ShouldReturnResult()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<ClientPaginatedList<CoinResult>>>("/api/Coin");

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
        public async Task GetCoinBySymbol_ShouldReturnResult(string symbol)
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<CoinResult>>("/api/Coin/Symbol/" + symbol);

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Symbol.Should().Be(symbol);
        }

        [Theory]
        [InlineData("OOO")]
        public async Task GetCoinBySymbol_ShouldReturnNoResult(string symbol)
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<CoinResult>>("/api/Coin/Symbol/" + symbol);

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().BeNull();
        }
    }
}