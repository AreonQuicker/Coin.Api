using CoinApi.Domain.CryptoCurrency.Models;
using MediatR;

namespace CoinApi.Application.Core.CryptoCurrency.Queries
{
    public class GetCryptoCurrencyBySymbolQuery : IRequest<CryptoCurrencyResult>
    {
        public GetCryptoCurrencyBySymbolQuery(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}