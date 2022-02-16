using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Coin.Queries;
using CoinApi.Domain.Coin.Interfaces;
using CoinApi.Domain.Coin.Models;
using MediatR;

namespace CoinApi.Application.Coin.Queries
{
    /// <summary>
    /// Get coin by symbol from database
    /// </summary>
    public class GetCoinBySymbolQueryHandler : IRequestHandler<GetCoinBySymbolQuery, CoinResult>
    {
        private readonly ICoinService _coinService;

        public GetCoinBySymbolQueryHandler(ICoinService coinService)
        {
            _coinService = coinService;
        }

        public Task<CoinResult> Handle(GetCoinBySymbolQuery request, CancellationToken cancellationToken)
        {
            return _coinService.GetBySymbolAsync(request.Symbol);
        }
    }
}