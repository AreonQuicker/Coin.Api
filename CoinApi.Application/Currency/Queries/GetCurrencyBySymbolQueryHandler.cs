using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Currency.Queries;
using CoinApi.Domain.Currency.Interfaces;
using CoinApi.Domain.Currency.Models;
using MediatR;

namespace CoinApi.Application.Currency.Queries
{
    /// <summary>
    /// Get currency by symbol from databae
    /// </summary>
    public class GetCurrencyBySymbolQueryHandler : IRequestHandler<GetCurrencyBySymbolQuery, CurrencyResult>
    {
        private readonly ICurrencyService _currencyService;

        public GetCurrencyBySymbolQueryHandler(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public Task<CurrencyResult> Handle(GetCurrencyBySymbolQuery request, CancellationToken cancellationToken)
        {
            return _currencyService.GetBySymbolAsync(request.Symbol);
        }
    }
}