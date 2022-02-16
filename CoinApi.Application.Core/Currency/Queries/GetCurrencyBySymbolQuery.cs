using CoinApi.Domain.Currency.Models;
using MediatR;

namespace CoinApi.Application.Core.Currency.Queries
{
    public class GetCurrencyBySymbolQuery : IRequest<CurrencyResult>
    {
        public GetCurrencyBySymbolQuery(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}