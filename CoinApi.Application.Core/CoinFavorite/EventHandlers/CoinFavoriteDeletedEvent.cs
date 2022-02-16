using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.EventHandlers
{
    public class CoinFavoriteDeletedEvent : INotification
    {
        public CoinFavoriteDeletedEvent(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}