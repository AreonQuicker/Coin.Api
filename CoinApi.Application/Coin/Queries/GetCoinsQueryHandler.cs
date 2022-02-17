using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Coin.Queries;
using CoinApi.Domain.Coin.Interfaces;
using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Models;
using MediatR;

namespace CoinApi.Application.Coin.Queries
{
    /// <summary>
    /// Get paginated coins from database
    /// </summary>
    public class GetCoinsQueryHandler : IRequestHandler<GetCoinsQuery, PaginatedList<CoinResult>>
    {
        private readonly IGetPaginatedCoinsFluentService _getPaginatedCoinsFluentService;

        public GetCoinsQueryHandler(IGetPaginatedCoinsFluentService getPaginatedCoinsFluentService)
        {
            _getPaginatedCoinsFluentService = getPaginatedCoinsFluentService;
        }

        public Task<PaginatedList<CoinResult>> Handle(GetCoinsQuery request, CancellationToken cancellationToken)
        {

            return
                _getPaginatedCoinsFluentService
                    .WithRank(request.Rank)
                    .WithSymbol(request.Symbol)
                    .WithPaginatedInfo((request.PageNumber, request.PageSize))
                    .WithSortInfo(new SortInfo(request.SortKey, request.SortType))
                    .GetAsync();
        }
    }
}