using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Currency.Models;
using CoinApi.Domain.Logic.Currency.Services;
using CoinApi.Mapping.Profiles;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoinApi.IntegrationTests.Tests
{
    [Collection("Application")]
    public class CurrencyServiceTests : TestBase, IClassFixture<TestFactory>
    {
        public CurrencyServiceTests(TestFactory testFactory) : base(testFactory)
        {
            _dataContext = TestHelpers.CreateDbContext();
            TestHelpers.SeedDataAsync(_dataContext).GetAwaiter().GetResult();
        }

        private DataContext _dataContext { get; }

        [Theory]
        [InlineAutoData("")]
        [InlineAutoData("OOO")]
        public async Task GetCurrencyBySymbol_ShouldReturnNull_WithInValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<CurrencyService>();

            var result = await coinService.GetBySymbolAsync(symbol);

            result.Should().BeNull();
        }

        [Theory]
        [InlineAutoData("Symbol")]
        [InlineAutoData("Symbol2")]
        public async Task GetCurrencyBySymbol_ShouldReturnNotNull_WithValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<CurrencyService>();

            var result = await coinService.GetBySymbolAsync(symbol);

            result.Should().NotBeNull();
            result.Symbol.Should().Be(symbol);
        }

        [Theory]
        [InlineAutoData("Symbol")]
        [InlineAutoData("Symbol2")]
        public async Task GetCurrencysPaginated_ShouldReturnResult_WithValidValues(string symbol, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<GetPaginatedCurrenciesFluentService>();

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
        public async Task GetCurrencysPaginated_ShouldReturnEmptyResult_WithInValidValues(string symbol,
            IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<GetPaginatedCurrenciesFluentService>();

            var result = await coinService.WithSymbol(symbol).WithPaginatedInfo((1, null)).GetAsync();

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData]
        public async Task GetCurrencysPaginated_ShouldReturnEmptyResult_WithInValidPageValues(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<GetPaginatedCurrenciesFluentService>();

            var result = await coinService.WithPaginatedInfo((2, 10)).GetAsync();

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData("Symbol3", "Name")]
        [InlineAutoData("Symbol4", "Name")]
        public async Task DeleteAndAddCurrencyds_ShouldAddInDb_WithValidValues(string symbol, string name,
            IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<DeleteAndAddCurrencyFluentService>();

            var coinCreateRequest = new List<CurrencyDeleteAndAddRequest>
            {
                new() {Name = name, Symbol = symbol, Sign = ""}
            };

            await coinService.WithRequest(coinCreateRequest).ExecuteAsync();

            var result = await _dataContext.Currencies.ToListAsync();

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
        public async Task DeleteAndAddCurrencyds_ShouldNotAddInDbButUpdate_WithValidValues(string symbol, string name,
            IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<DeleteAndAddCurrencyFluentService>();

            var coinCreateRequest = new List<CurrencyDeleteAndAddRequest>
            {
                new() {Name = name, Symbol = symbol, Sign = ""}
            };

            await coinService
                .WithRequest(coinCreateRequest)
                .ExecuteAsync();

            var result = await _dataContext.Currencies.ToListAsync();

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
        public async Task DeleteAndAddCurrencyds_ShouldThrowException_WithInValidValues(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            fixture.Inject(_dataContext);
            fixture.Inject(mapper);

            var coinService = fixture.Create<DeleteAndAddCurrencyFluentService>();

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