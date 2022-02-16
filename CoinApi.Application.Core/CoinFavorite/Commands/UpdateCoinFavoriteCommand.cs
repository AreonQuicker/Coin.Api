using CoinApi.Domain.CoinFavorite.Models;
using CoinApi.Domain.Common.Mapping.Interfaces;
using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.Commands
{
    public class UpdateCoinFavoriteCommand : CoinFavoriteUpdateRequest, IRequest<int>,
        IMapFrom<CoinFavoriteUpdateRequest>
    {
        public UpdateCoinFavoriteCommand(string symbol)
        {
            Symbol = symbol;
        }

        public UpdateCoinFavoriteCommand()
        {
        }

        public string Symbol { get; set; }
    }
}