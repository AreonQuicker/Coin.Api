using CoinApi.Domain.Coin.Models;
using MediatR;

namespace CoinApi.Application.Core.Coin.Queries
{
    public class GetCoinBySymbolQuery : IRequest<CoinResult>
    {
        public GetCoinBySymbolQuery(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}