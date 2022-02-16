using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Logic.Coin.Services;
using CoinApi.Mapping.Profiles;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static CoinApi.IntegrationTests.TestHelpers;

namespace CoinApi.IntegrationTests.Tests
{
    [Collection("Application")]
    public class CoinServiceTests : TestBase, IClassFixture<TestFactory>
    {
        public CoinServiceTests(TestFactory testFactory) : base(testFactory)
        {
            _dataContext = CreateDbContext();
            SeedDataAsync(_dataContext).GetAwaiter().GetResult();
        }

        private DataContext _dataContext { get; }

        [Theory]
        [InlineAutoData("")]
        [InlineAutoData("OOO")]
        public async Task GetCoinBySymbol_ShouldReturnNull_WithInValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<CoinService>();

            var result = await coinService.GetBySymbolAsync(symbol);

            result.Should().BeNull();
        }

        [Theory]
        [InlineAutoData("Symbol")]
        [InlineAutoData("Symbol2")]
        public async Task GetCoinBySymbol_ShouldReturnNotNull_WithValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<CoinService>();

            var result = await coinService.GetBySymbolAsync(symbol);

            result.Should().NotBeNull();
            result.Symbol.Should().Be(symbol);
        }

        [Theory]
        [InlineAutoData("Symbol")]
        [InlineAutoData("Symbol2")]
        public async Task GetCoinsPaginated_ShouldReturnResult_WithValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<GetPaginatedCoinsFluentService>();

            var result = await coinService.WithSymbol(symbol).GetAsync();

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Should().NotBeEmpty();
            result.Items.Should().Contain(a => a.Symbol == symbol);
            result.Items.Should().HaveCount(1);
        }

        [Theory]
        [InlineAutoData("")]
        [InlineAutoData("OOO")]
        public async Task GetCoinsPaginated_ShouldReturnEmptyResult_WithInValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<GetPaginatedCoinsFluentService>();

            var result = await coinService.WithSymbol(symbol).WithPaginatedInfo((1, null)).GetAsync();

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData]
        public async Task GetCoinsPaginated_ShouldReturnEmptyResult_WithInValidPageValues(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<GetPaginatedCoinsFluentService>();

            var result = await coinService.WithPaginatedInfo((2, 10)).GetAsync();

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData("Symbol3", "Name")]
        [InlineAutoData("Symbol4", "Name")]
        public async Task DeleteAndAddCoinds_ShouldAddInDb_WithValidValues(string symbol, string name, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<DeleteAndAddCoinFluentService>();

            var coinCreateRequest = new List<CoinCreateRequest>
            {
                new() {Name = name, Rank = 1, Slug = "Slug", Symbol = symbol}
            };

            await coinService.WithRequest(coinCreateRequest).ExecuteAsync();

            var result = await _dataContext.Coins.ToListAsync();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
            result.Should().Contain(c => c.Symbol == symbol);
            result.Where(w => w.Symbol == symbol).Should()
                .BeEquivalentTo(coinCreateRequest,
                    o => o.ExcludingNestedObjects()
                        .ExcludingMissingMembers()
                        .Excluding(ex => ex.Created)
                        .Excluding(ex => ex.Id)
                        .Excluding(ex => ex.LastModified));
        }

        [Theory]
        [InlineAutoData("Symbol", "Name3")]
        [InlineAutoData("Symbol2", "Name3")]
        public async Task DeleteAndAddCoinds_ShouldNotAddInDbButUpdate_WithValidValues(string symbol, string name,
            IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<DeleteAndAddCoinFluentService>();

            var coinCreateRequest = new List<CoinCreateRequest>
            {
                new() {Name = name, Rank = 1, Slug = "Slug", Symbol = symbol}
            };

            await coinService
                .WithRequest(coinCreateRequest)
                .ExecuteAsync();

            var result = await _dataContext.Coins.ToListAsync();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);

            result.Where(w => w.Symbol == symbol).Should()
                .BeEquivalentTo(coinCreateRequest,
                    o => o.ExcludingNestedObjects()
                        .ExcludingMissingMembers()
                        .Excluding(ex => ex.Created)
                        .Excluding(ex => ex.Id)
                        .Excluding(ex => ex.LastModified));
        }

        [Theory]
        [AutoData]
        public async Task DeleteAndAddCoinds_ShouldThrowException_WithInValidValues(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<DeleteAndAddCoinFluentService>();

            Func<Task> f = async () =>
            {
                await coinService
                    .WithRequest(null)
                    .ExecuteAsync();
            };
            await f.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}