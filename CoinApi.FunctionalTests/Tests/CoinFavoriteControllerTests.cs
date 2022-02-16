using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using CoinApi.Application.Core.CoinFavorite.Commands;
using CoinApi.Domain.CoinFavorite.Models;
using CoinApi.FunctionalTests.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CoinApi.FunctionalTests.Tests
{
    [Collection("CoinFavoriteControllerTests")]
    public class CoinFavoriteControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CoinFavoriteControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCoinFavorites_ShouldReturnResult()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<IList<CoinFavoriteResult>>>("/api/CoinFavorite");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCoinFavorite_ShouldReturnResult()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<CoinFavoriteResult>>(
                    "/api/CoinFavorite/Symbol/Symbol");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().NotBeNull();
            result.Data.Symbol.Should().Be("Symbol");
        }

        [Fact]
        public async Task GetCoinFavorite_ShouldReturnNoResult()
        {
            var result =
                await _client.GetAndDeserialize<ClientActionResult<CoinFavoriteResult>>("/api/CoinFavorite/Symbol/OOO");

            result.Should().NotBeNull();
            result.Failed.Should().Be(false);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task CreateCoinFavorite_ShouldReturnNewCreatedId_WithValidValues()
        {
            var result =
                await _client.PostAsJsonAsync("/api/CoinFavorite", new CreateCoinFavoriteCommand
                {
                    Symbol = "Symbol2",
                    FavoriteCurrencies = new List<CurrencyFavoriteCreateRequest>
                    {
                        new()
                        {
                            Symbol = "Symbol2"
                        }
                    }
                });

            var jsonString = await result.Content.ReadAsStringAsync();
            var contResult = JsonConvert.DeserializeObject<ClientActionResult<int>>(jsonString);

            contResult.Should().NotBeNull();
            contResult.Failed.Should().Be(false);
            contResult.Data.Should().Be(3);
        }

        [Fact]
        public async Task CreateCoinFavorite_ShouldFailed_WithInvalidValues()
        {
            var result =
                await _client.PostAsJsonAsync("/api/CoinFavorite", new CreateCoinFavoriteCommand
                {
                    Symbol = "Symbol3",
                    FavoriteCurrencies = new List<CurrencyFavoriteCreateRequest>
                    {
                        new()
                        {
                            Symbol = "Symbol3"
                        }
                    }
                });

            var jsonString = await result.Content.ReadAsStringAsync();
            var contResult = JsonConvert.DeserializeObject<ClientActionResult>(jsonString);

            contResult.Should().NotBeNull();
            contResult.Failed.Should().Be(true);
        }

        [Fact]
        public async Task DeleteCoinFavorite_ShouldFailed_WithInvalidValues()
        {
            var result =
                await _client.DeleteAsync("/api/CoinFavorite/Symbol/OOO");

            var jsonString = await result.Content.ReadAsStringAsync();
            var contResult = JsonConvert.DeserializeObject<ClientActionResult>(jsonString);

            contResult.Should().NotBeNull();
            contResult.Failed.Should().Be(true);
        }
    }
}