using System;
using System.Threading;
using CoinApi.Application.Core.Coin.Commands;
using CoinApi.Application.Core.CryptoCurrency.Commands;
using CoinApi.Application.Core.Currency.Commands;
using CoinApi.Hangfire.Core.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.Hangfire.Services
{
    public class CoinHangfireService : ICoinHangfireService
    {
        private readonly IServiceProvider _serviceProvider;

        public CoinHangfireService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [AutomaticRetry(Attempts = 0)]
        public void SyncCoins(CancellationToken token)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                sender.Send(new SyncCoinsCommand(), token).GetAwaiter().GetResult();
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public void SyncCurrencies(CancellationToken token)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                sender.Send(new SyncCurrenciesCommand(), token).GetAwaiter().GetResult();
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public void SyncCryptoCurrencies(CancellationToken token)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                sender.Send(new SyncCryptoCurrenciesCommand(), token).GetAwaiter().GetResult();
            }
        }

        [AutomaticRetry(Attempts = 0)]
        [Queue("alpha")]
        public void SyncCryptoCurrency(string[] keys, CancellationToken token)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                sender.Send(new SyncCryptoCurrencyCommand(keys), token).GetAwaiter().GetResult();
            }
        }
    }
}