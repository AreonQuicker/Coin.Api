using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CoinFavorite.Commands;
using CoinApi.Application.Core.CoinFavorite.EventHandlers;
using CoinApi.Domain.CoinFavorite.Interfaces;
using MediatR;

namespace CoinApi.Application.CoinFavorite.Commands
{
    public class CoinFavoriteCommandHandler : IRequestHandler<CreateCoinFavoriteCommand, int>,
        IRequestHandler<UpdateCoinFavoriteCommand, int>, IRequestHandler<DeleteCoinFavoriteBySymbolCommand, int>
    {
        private readonly ICoinFavoriteService _coinFavoriteService;
        private readonly IPublisher _publisher;

        public CoinFavoriteCommandHandler(ICoinFavoriteService coinFavoriteService, IPublisher publisher)
        {
            _coinFavoriteService = coinFavoriteService;
            _publisher = publisher;
        }

        /// <summary>
        /// Create coin favorite in database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(CreateCoinFavoriteCommand request, CancellationToken cancellationToken)
        {
            var id = await _coinFavoriteService.CreateAsync(request);

            //Fire event after coin favorite has been created to sync crypto currency
            await _publisher.Publish(new CoinFavoriteCreatedEvent(request.Symbol), cancellationToken);

            return id;
        }

        /// <summary>
        /// Delete coin favorite from database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(DeleteCoinFavoriteBySymbolCommand request, CancellationToken cancellationToken)
        {
            var id = await _coinFavoriteService.DeleteBySymbolAsync(request.Symbol);

            //Fire event after coin favorite has been deleted to sync crypto currency
            await _publisher.Publish(new CoinFavoriteDeletedEvent(request.Symbol), cancellationToken);

            return id;
        }

        /// <summary>
        /// Update coin favorite in database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(UpdateCoinFavoriteCommand request, CancellationToken cancellationToken)
        {
            var id = await _coinFavoriteService.UpdateAsync(request.Symbol, request);

            //Fire event after coin favorite has been updated to sync crypto currency
            await _publisher.Publish(new CoinFavoriteUpdatedEvent(request.Symbol), cancellationToken);

            return id;
        }
    }
}