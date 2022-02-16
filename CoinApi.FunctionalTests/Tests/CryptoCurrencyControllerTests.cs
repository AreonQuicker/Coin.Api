using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using CoinApi.Domain.CryptoCurrency.Models;
using CoinApi.FunctionalTests.Models;
using FluentAssertions;
using Xunit;

namespace CoinApi.FunctionalTests.Tests
{
    [Collection("CryptoCurrencyControllerTests")]
    public class CryptoCurrencyControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CryptoCurrencyControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCryptoCurrencies_ShouldReturnResult()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<ClientPaginatedList<CryptoCurrencyResult>>>(
                    "/api/CryptoCurrency");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Items.Should().NotBeNull();
            result.Data.Items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCryptoCurrencies_ShouldReturnOneResult_WithValidValue()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<ClientPaginatedList<CryptoCurrencyResult>>>(
                    "/api/CryptoCurrency?Symbol=Symbol");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Items.Should().NotBeNull();
            result.Data.Items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCryptoCurrencies_ShouldReturnNoResult_WithValidValue()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<ClientPaginatedList<CryptoCurrencyResult>>>(
                    "/api/CryptoCurrency?Symbol=OOO");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Items.Should().NotBeNull();
            result.Data.Items.Should().BeEmpty();
        }
    }
}