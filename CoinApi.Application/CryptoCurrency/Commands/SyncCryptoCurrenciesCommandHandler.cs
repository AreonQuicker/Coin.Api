using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CryptoCurrency.Commands;
using CoinApi.Background.Queue.Core.Enums;
using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.Background.Queue.Core.Requests;
using CoinApi.Domain.CoinFavorite.Interfaces;
using MediatR;

namespace CoinApi.Application.CryptoCurrency.Commands
{
    /// <summary>
    /// Sync crypto rates that have been favorite from client to database
    /// </summary>
    public class SyncCryptoCurrenciesCommandHandler : IRequestHandler<SyncCryptoCurrenciesCommand>
    {
        private readonly IBackgroundQueue _backgroundQueue;
        private readonly ICoinFavoriteService _coinFavoriteService;
        private readonly ISender _sender;

        public SyncCryptoCurrenciesCommandHandler(ICoinFavoriteService coinFavoriteService, ISender sender,
            IBackgroundQueue backgroundQueue)
        {
            _coinFavoriteService = coinFavoriteService;
            _sender = sender;
            _backgroundQueue = backgroundQueue;
        }

        /// <summary>
        /// Get all coins that been favorite and queue it up to be sync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SyncCryptoCurrenciesCommand request, CancellationToken cancellationToken)
        {
            var coinFavorites = await _coinFavoriteService.GetAsync();

            var chunks = coinFavorites.Chunk(10);

            foreach (var chunk in chunks)
            {
                foreach (var coinFavorite in coinFavorites)
                    await _backgroundQueue.QueueAsync(
                        new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCryptoCurrency,
                            chunk.Select(s => s.Symbol).ToArray()));
            }

            return Unit.Value;
        }
    }
}