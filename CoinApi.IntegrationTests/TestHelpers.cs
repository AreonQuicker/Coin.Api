using System.Collections.Generic;
using System.Threading.Tasks;
using CoinApi.DataAccess;
using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.IntegrationTests
{
    public static class TestHelpers
    {
        public static DataContext CreateDbContext()
        {
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase("cleanarchitecture")
                .UseInternalServiceProvider(serviceProvider);

            var options = builder.Options;

            var dbContext = new DataContext(options);

            return dbContext;
        }

        public static async Task SeedDataAsync(DataContext dataContext)
        {
            await dataContext.Coins.AddRangeAsync(new CoinDomainModel
            {
                Name = "Name",
                Rank = 1,
                Slug = "Slug",
                Symbol = "Symbol"
            }, new CoinDomainModel
            {
                Name = "Name2",
                Rank = 1,
                Slug = "Slug2",
                Symbol = "Symbol2"
            });

            await dataContext.Currencies.AddRangeAsync(new CurrencyDomainModel
            {
                Name = "Name",
                Symbol = "Symbol",
                Sign = ""
            }, new CurrencyDomainModel
            {
                Name = "Name2",
                Symbol = "Symbol2",
                Sign = ""
            });

            await dataContext.CoinFavorites.AddAsync(new CoinFavoriteDomainModel
            {
                Symbol = "Symbol",
                FavoriteCurrencies = new List<CurrencyFavoriteDomainModel>
                {
                    new()
                    {
                        Symbol = "Symbol"
                    }
                }
            });

            await dataContext.CoinFavorites.AddAsync(new CoinFavoriteDomainModel
            {
                Symbol = "Symbol3",
                FavoriteCurrencies = new List<CurrencyFavoriteDomainModel>
                {
                    new()
                    {
                        Symbol = "Symbol3"
                    }
                }
            });

            await dataContext.CryptoCurrencies.AddAsync(new CryptoCurrencyDomainModel
            {
                Slug = "Slug",
                Symbol = "Symbol",
                CmcRank = 1,
                NumMarketPairs = 1,
                Quotes = new List<CryptoCurrencyQuoteDomainModel>
                {
                    new()
                    {
                        Price = 1,
                        Symbol = "Symbol",
                        MarketCap = 1,
                        Volume24H = 1,
                        CryptoCurrencyId = 1
                    }
                }
            });

            await dataContext.SaveChangesAsync();
        }
    }
}