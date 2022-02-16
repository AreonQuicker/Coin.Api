using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.Commands
{
    public class DeleteCoinFavoriteBySymbolCommand : IRequest<int>
    {
        public DeleteCoinFavoriteBySymbolCommand(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}