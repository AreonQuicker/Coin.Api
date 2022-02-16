using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CoinFavorite.EventHandlers;
using CoinApi.Application.Core.CryptoCurrency.Commands;
using CoinApi.Background.Queue.Core.Enums;
using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.Background.Queue.Core.Requests;
using MediatR;

namespace CoinApi.Application.CoinFavorite.EventHandlers
{
    public class CoinFavoriteEventHandler : INotificationHandler<CoinFavoriteCreatedEvent>,
        INotificationHandler<CoinFavoriteUpdatedEvent>, INotificationHandler<CoinFavoriteDeletedEvent>
    {
        private readonly IBackgroundQueue _backgroundQueue;
        private readonly ISender _sender;

        public CoinFavoriteEventHandler(IBackgroundQueue backgroundQueue, ISender sender)
        {
            _backgroundQueue = backgroundQueue;
            _sender = sender;
        }

        /// <summary>
        /// Created coin favorite event. Queue up request to sync crypto rate
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(CoinFavoriteCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _backgroundQueue.QueueAsync(new BackgroundQueueRequest(
                BackgroundQueueRequestTypeEnum.SyncCryptoCurrency,
                notification.Symbol));
        }

        /// <summary>
        /// Deleted coin favorite event. Delete crypto rate
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(CoinFavoriteDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteCryptoCurrencyBySymbolCommand(notification.Symbol));
        }

        /// <summary>
        /// Updated coin favorite event. Queue up request to sync crypto rate
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(CoinFavoriteUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _backgroundQueue.QueueAsync(new BackgroundQueueRequest(
                BackgroundQueueRequestTypeEnum.SyncCryptoCurrency,
                notification.Symbol));
        }
    }
}