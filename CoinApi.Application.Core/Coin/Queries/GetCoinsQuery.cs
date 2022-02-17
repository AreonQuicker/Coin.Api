using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Enums;
using CoinApi.Domain.Common.Models;
using MediatR;

namespace CoinApi.Application.Core.Coin.Queries
{
    public class GetCoinsQuery : IRequest<PaginatedList<CoinResult>>
    {
        public GetCoinsQuery(int pageNumber = 1, int? pageSize = null, string sortKey = null,SortOrderTypeEnum sortType = SortOrderTypeEnum.ASC)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortKey = sortKey;
            SortType = sortType;
        }

        public int PageNumber { get; }
        public int? PageSize { get; }
        public string SortKey { get; }
        public SortOrderTypeEnum SortType { get; }
        public int? Rank { get; init; }
        public string Symbol { get; init; }
    }
}