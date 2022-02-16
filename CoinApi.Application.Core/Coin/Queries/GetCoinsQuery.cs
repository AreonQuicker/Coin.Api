using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Models;
using MediatR;

namespace CoinApi.Application.Core.Coin.Queries
{
    public class GetCoinsQuery : IRequest<PaginatedList<CoinResult>>
    {
        public GetCoinsQuery(int pageNumber = 1, int? pageSize = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }
        public int? PageSize { get; }
        public int? Rank { get; init; }
        public string Symbol { get; init; }
    }
}