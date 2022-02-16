using CoinApi.Domain.Coin.Interfaces;
using CoinApi.Domain.CoinFavorite.Interfaces;
using CoinApi.Domain.CryptoCurrency.Interfaces;
using CoinApi.Domain.Currency.Interfaces;
using CoinApi.Domain.Logic.Coin.Services;
using CoinApi.Domain.Logic.CoinFavorite.Services;
using CoinApi.Domain.Logic.CryptoCurrency.Services;
using CoinApi.Domain.Logic.Currency.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.Domain.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainLogic(this IServiceCollection services)
        {
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICoinFavoriteService, CoinFavoriteService>();
            services.AddScoped<ICryptoCurrencyService, CryptoCurrencyService>();

            services.AddTransient<IDeleteAndAddCoinFluentService, DeleteAndAddCoinFluentService>();
            services.AddTransient<IDeleteAndAddCurrencyFluentService, DeleteAndAddCurrencyFluentService>();

            services.AddTransient<IGetPaginatedCoinsFluentService, GetPaginatedCoinsFluentService>();
            services.AddTransient<IGetPaginatedCurrenciesFluentService, GetPaginatedCurrenciesFluentService>();
            services
                .AddTransient<IGetPaginatedCryptoCurrenciesFluentService, GetPaginatedCryptoCurrenciesFluentService>();

            return services;
        }
    }
}