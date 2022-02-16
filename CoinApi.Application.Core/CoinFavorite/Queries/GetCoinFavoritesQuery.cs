using System.Collections.Generic;
using CoinApi.Domain.CoinFavorite.Models;
using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.Queries
{
    public class GetCoinFavoritesQuery : IRequest<IList<CoinFavoriteResult>>
    {
    }
}