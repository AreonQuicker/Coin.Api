using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.EventHandlers
{
    public class CoinFavoriteUpdatedEvent : INotification
    {
        public CoinFavoriteUpdatedEvent(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}