using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CryptoCurrency.Commands;
using CoinApi.Application.Core.Integration.Queries;
using CoinApi.Domain.CoinFavorite.Interfaces;
using CoinApi.Domain.Common.Constants.ErrorCodes;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Domain.CryptoCurrency.Interfaces;
using CoinApi.Domain.CryptoCurrency.Models;
using CoinApi.Domain.Models.DomainModels;
using MediatR;

namespace CoinApi.Application.CryptoCurrency.Commands
{
    /// <summary>
    /// Sync crypto rate from outside client to database
    /// </summary>
    public class SyncCryptoCurrencyCommandHandler : IRequestHandler<SyncCryptoCurrencyCommand>
    {
        private readonly ICoinFavoriteService _coinFavoriteService;
        private readonly ICryptoCurrencyService _cryptoCurrencyService;
        private readonly ISender _sender;

        public SyncCryptoCurrencyCommandHandler(ICoinFavoriteService coinFavoriteService, ISender sender,
            ICryptoCurrencyService cryptoCurrencyService)
        {
            _coinFavoriteService = coinFavoriteService;
            _sender = sender;
            _cryptoCurrencyService = cryptoCurrencyService;
        }

        public async Task<Unit> Handle(SyncCryptoCurrencyCommand request, CancellationToken cancellationToken)
        {
            List<CryptoCurrencyUpsertRequest> upsertRequests = new List<CryptoCurrencyUpsertRequest>();

            foreach (var symbol in request.Symbols)
            {
                //Get coin favorite by symbol to confirm that that the coin has been favorite
                var coinFavorite = await _coinFavoriteService.GetBySymbolAsync(symbol);

                if (coinFavorite is null)
                    throw new NotFoundException(nameof(coinFavorite), symbol,
                        ServiceErrorCodesConstant.CoinFavoriteNotFound);
                
                //Fetch crypto rate from outside client
                var rate = await _sender.Send(new GetCryptocurrencyFromIntegrationClientQuery(coinFavorite.Symbol,
                    coinFavorite.FavoriteCurrencies.Select(s => s.Symbol).ToList()));
                
                var result = rate.Data.Values.FirstOrDefault();

                if (result?.Quotes is null || !result.Quotes.Any())
                    throw new NotFoundException(nameof(rate), symbol,
                        ServiceErrorCodesConstant.RateNotFound);

                var upsertRequest = new CryptoCurrencyUpsertRequest
                {
                    Slug = result.Slug,
                    Symbol = result.Symbol,
                    CmcRank = result.CmcRank,
                    NumMarketPairs = result.NumMarketPairs,
                    Quotes = result.Quotes?.Select(s => new CryptoCurrencyQuoteDomainModel
                    {
                        Price = s.Value.Price,
                        Symbol = s.Key,
                        MarketCap = s.Value.MarketCap,
                        Volume24H = s.Value.Volume24H
                    }).ToList()
                };
                
                upsertRequests.Add(upsertRequest);
            }

            //Save crypto rate in database  
            await _cryptoCurrencyService.UpsertAsync(upsertRequests.ToArray());

            return Unit.Value;
        }
    }
}