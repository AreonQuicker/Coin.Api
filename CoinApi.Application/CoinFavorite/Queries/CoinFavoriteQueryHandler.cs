using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CoinFavorite.Queries;
using CoinApi.Domain.CoinFavorite.Interfaces;
using CoinApi.Domain.CoinFavorite.Models;
using MediatR;

namespace CoinApi.Application.CoinFavorite.Queries
{
    public class CoinFavoriteQueryHandler : IRequestHandler<GetCoinFavoriteBySymbolQuery, CoinFavoriteResult>,
        IRequestHandler<GetCoinFavoritesQuery, IList<CoinFavoriteResult>>
    {
        private readonly ICoinFavoriteService _coinFavoriteService;

        public CoinFavoriteQueryHandler(ICoinFavoriteService coinFavoriteService)
        {
            _coinFavoriteService = coinFavoriteService;
        }

        /// <summary>
        /// Get coin favorite by symbol from database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<CoinFavoriteResult> Handle(GetCoinFavoriteBySymbolQuery request,
            CancellationToken cancellationToken)
        {
            return _coinFavoriteService.GetBySymbolAsync(request.Symbol);
        }

        /// <summary>
        /// Get all coin favorites from database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IList<CoinFavoriteResult>> Handle(GetCoinFavoritesQuery request,
            CancellationToken cancellationToken)
        {
            return _coinFavoriteService.GetAsync();
        }
    }
}