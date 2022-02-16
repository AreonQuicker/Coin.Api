using CoinApi.Domain.CoinFavorite.Models;
using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.Queries
{
    public class GetCoinFavoriteBySymbolQuery : IRequest<CoinFavoriteResult>
    {
        public GetCoinFavoriteBySymbolQuery(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}