using CoinApi.Domain.CoinFavorite.Models;
using MediatR;

namespace CoinApi.Application.Core.CoinFavorite.Commands
{
    public class CreateCoinFavoriteCommand : CoinFavoriteCreateRequest, IRequest<int>
    {
    }
}