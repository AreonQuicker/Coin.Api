using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.EventHandlers
{
    public class CoinFavoriteCreatedEvent : INotification
    {
        public CoinFavoriteCreatedEvent(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}